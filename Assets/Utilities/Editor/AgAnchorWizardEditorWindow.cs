
// ===================================================================================
// Copyright (c) 2016, Abstraction Games B.V.
// ===================================================================================

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;

// ===================================================================================

/// <summary>
/// WARNING: 
/// - Currently only works in Screen Space Overlay canvassi!
/// - Currently only works if the center of the "to-be-anchored-object" is within the anchors of its parent, 
///   otherwise will be snapped to be on the edge of the parent's boundaries.
/// You can always switch to screen space overlay, apply the anchors, and switch back to achieve results regardless of the first flaw.
/// </summary>
public class AgAnchorWizardEditorWindow : EditorWindow
{
    public List<RectTransform> m_rectTransforms = new List<RectTransform>();

    // ===============================================================================

    [MenuItem( "AG/UI: Anchor Wizard" )]
    public static void ShowWindow()
    {
        GetWindow( typeof( AgAnchorWizardEditorWindow ) );
    }

    // ===============================================================================

    void OnGUI()
    {
        GUI.color = Color.white;

        // Retrieve selected rectTransforms.
        m_rectTransforms.Clear();
        foreach( GameObject go in Selection.gameObjects )
        {
            RectTransform temp = go.GetComponent<RectTransform>();
            if (temp != null)
            {
                m_rectTransforms.Add( temp );
            }
        }

        // Check retrieval.
        if( m_rectTransforms.Count == 0 )
        {
            AgEditorUtils.Header( "RectTransform Retrieval" );
            EditorGUILayout.HelpBox( "No rect transforms found to snap.", MessageType.Info );
        }
        else
        {
            // Show utilities.
            AgEditorUtils.Header( "RectTransform Utilities" );
            if (GUILayout.Button("Match anchors to RectTransform Positions"))
            {
                foreach( RectTransform rectTrans in m_rectTransforms )
                {
                    Undo.RecordObject( rectTrans, "Matching anchors for " + rectTrans.name );
                    Debug.Log( "Closing in: " + rectTrans.name );
                    _matchAnchorsToPosition( rectTrans );
                    EditorUtility.SetDirty( rectTrans );
                }
            }
        }
    }

    // ===============================================================================

    private void _matchAnchorsToPosition( RectTransform rectTrans )
    {
        RectTransform parentRectTransform = rectTrans.parent.GetComponent<RectTransform>();
        Rect parentRect = parentRectTransform.rect;
        float parentWidth = parentRect.width;
        float parentHeight = parentRect.height;

        float leftX = parentRect.center.x - parentWidth / 2.0f;
        float rightX = parentRect.center.x + parentWidth / 2.0f;
        float bottomY = parentRect.center.y - parentHeight / 2.0f;
        float topY = parentRect.center.y + parentHeight / 2.0f;

        // Doing the following makes this work for Rects that don't have centered anchors (where anchor values aren't all 0.5f)
        // vs just using anchoredPosition.
        Vector2 position = rectTrans.position - parentRectTransform.position;

        float partialX01 = Mathf.InverseLerp( leftX, rightX, position.x );
        float paritalY01 = Mathf.InverseLerp( bottomY, topY, position.y );

        Vector2 partialSize01 = new Vector2(rectTrans.rect.width / parentRect.width, rectTrans.rect.height / parentRect.height);
        Vector2 halfPartialSize01 = partialSize01 / 2.0f;

        rectTrans.anchorMin = new Vector2( partialX01 - halfPartialSize01.x, paritalY01 - halfPartialSize01.y );
        rectTrans.anchorMax = new Vector2( partialX01 + halfPartialSize01.x, paritalY01 + halfPartialSize01.y );
        rectTrans.offsetMin = new Vector2( 0, 0 );
        rectTrans.offsetMax = new Vector2( 0, 0 );
    }
}