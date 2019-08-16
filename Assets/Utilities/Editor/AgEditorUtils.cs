
// ===================================================================================
// Copyright (c) 2016, Abstraction Games B.V.
// ===================================================================================

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Reflection;

// ===================================================================================

public class AgEditorUtils
{
    private static float m_HeaderSpace = 20f;

    // ===============================================================================

    public static void SetDirty(Object target)
    {
        EditorUtility.SetDirty( target );
        if( !Application.isPlaying )
        {
            EditorSceneManager.MarkSceneDirty( EditorSceneManager.GetActiveScene() );
        }
    }

    // ===============================================================================

    public static void Header(string label)
    {
        GUILayout.Space(m_HeaderSpace);
        BoldLabel(label);
    }

    // ===============================================================================

    public static void HeaderBox( string label, Color boxColor, Color textColor, bool showSpace = true )
    {
        // Color.
        GUI.color = boxColor;
        GUI.contentColor = textColor;

        // Space.
        if ( showSpace )
        {
            GUILayout.Space( m_HeaderSpace );
        }

        // Style.
        GUIStyle specialHeader = new GUIStyle( GUI.skin.button );
        specialHeader.alignment = TextAnchor.MiddleCenter;
        specialHeader.fontStyle = FontStyle.Bold;
        specialHeader.stretchWidth = true;

        // Label.
        GUILayout.Label( label, specialHeader );

        // Reset.
        GUI.color = Color.white;
        GUI.contentColor = Color.white;
    }

    // ===============================================================================

    public static void Border()
    {
        GUILayout.Label( "", GUI.skin.horizontalSlider );
    }

    // ===============================================================================

    public static void ColoredBorder( Color color )
    {
        GUI.color = color;
        Border();
        GUI.color = Color.white;
    }

    // ===============================================================================

    public static void Space(float distance = 20f)
    {
        GUILayout.Space( distance );
    }

    // ===============================================================================

    public static void BoldLabel(string label)
    {
        GUILayout.Label(label, EditorStyles.boldLabel);
    }

    // ===============================================================================

    public static void ColoredBoldLabel( string label, Color color )
    {
        GUIStyle s = new GUIStyle( EditorStyles.boldLabel );
        s.normal.textColor = color;

        GUILayout.Label( label, s, GUILayout.Height( EditorGUIUtility.singleLineHeight ) );
    }

    // ===============================================================================

    public static void ColoredBoldLabel( Rect r, string label, Color color )
    {
        GUIStyle s = new GUIStyle( EditorStyles.boldLabel );
        s.normal.textColor = color;
        s.fixedHeight = EditorGUIUtility.singleLineHeight;

        GUI.Label( r, label, s );
    }

    // ===============================================================================

    public static void ColoredSelectableBoldLabel( string label, Color color )
    {
        GUIStyle s = new GUIStyle( EditorStyles.boldLabel );
        s.normal.textColor = color;

        EditorGUILayout.SelectableLabel( label, s, GUILayout.Height( EditorGUIUtility.singleLineHeight ) );
    }

    // ===============================================================================

    public static void ColoredSelectableBoldLabelInline( string label, Color color )
    {
        GUIStyle s = new GUIStyle( EditorStyles.boldLabel );
        s.normal.textColor = color;
        Rect r = GUILayoutUtility.GetRect(Screen.width * 0.2f, EditorGUIUtility.singleLineHeight );

        EditorGUI.SelectableLabel( r, label, s );
    }

    // ===============================================================================

    public static void ProgressBar( float value, string label )
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(
            Screen.width * 0.4f,                // Min Width
            Screen.width,                       // Max Width
            EditorGUIUtility.singleLineHeight,  // Min Height
            EditorGUIUtility.singleLineHeight,  // Max Height
            "TextField",                        // Content
            GUILayout.ExpandWidth(true));       // Layout Option = Expand Width.

        EditorGUI.ProgressBar( rect, value, label );
        GUILayout.Space( 3 );
    }

    // ===============================================================================

    public static void ToggleButton(ref bool value, string label)
    {
        value = GUILayout.Toggle(value, label, GUI.skin.button);
    }

    // ===============================================================================

    public static void ToggleButton( ref float value, string label )
    {
        bool boolvalue = value > 0.0f;
        ToggleButton( ref boolvalue, label );
        value = boolvalue ? 1.0f : 0.0f;
    }

    // ===============================================================================

    public static void ToggleButton( ref bool value, string label, Color color )
    {
        GUI.color = color;
        value = GUILayout.Toggle( value, label, GUI.skin.button );
        GUI.color = Color.white;
    }

    // ===============================================================================

    public static void ToggleButtonAdvanced( ref bool value, string labelTrue, string labelFalse, Color colorTrue, Color colorFalse )
    {
        ToggleButton(
            ref value,
            value ? labelTrue : labelFalse,
            value ? colorTrue : colorFalse );
    }

    // ===============================================================================

    public static void ToggleButtonAdvanced( ref float value, string labelTrue, string labelFalse, Color colorTrue, Color colorFalse )
    {
        bool boolvalue = value > 0.0f;
        ToggleButtonAdvanced( ref boolvalue, labelTrue, labelFalse, colorTrue, colorFalse );
        value = boolvalue ? 1.0f : 0.0f;
    }

    // ===============================================================================

    public static void ToggleButtonInline( Rect rect, ref bool value, string label )
    {
        value = GUI.Toggle( rect, value, label, GUI.skin.button );
    }

    // ===============================================================================

    public static void ToggleButtonInline( Rect rect, ref float value, string label )
    {
        bool boolvalue = value > 0.0f;
        ToggleButtonInline( rect, ref boolvalue, label );
        value = boolvalue ? 1.0f : 0.0f;
    }

    // ===============================================================================

    private static void _toggleButtonInlineAdvanced( Rect rect, ref bool value, string label, Color color )
    {
        GUI.color = color;
        value = GUI.Toggle( rect, value, label, GUI.skin.button );
        GUI.color = Color.white;
    }

    // ===============================================================================

    public static void ToggleButtonInlineAdvanced( Rect rect, ref bool value, string labelTrue, string labelFalse, Color colorTrue, Color colorFalse )
    {
        _toggleButtonInlineAdvanced( 
            rect, 
            ref value, 
            value ? labelTrue : labelFalse, 
            value ? colorTrue : colorFalse );
    }

    // ===============================================================================

    public static void ToggleButtonInlineAdvanced( Rect rect, ref float value, string labelTrue, string labelFalse, Color colorTrue, Color colorFalse )
    {
        bool boolvalue = value > 0.0f;
        ToggleButtonInlineAdvanced( rect, ref boolvalue, labelTrue, labelFalse, colorTrue, colorFalse );
        value = boolvalue ? 1.0f : 0.0f;
    }

    // ===============================================================================

    /*
        Below code makes a two-sided toggle based on one value like this:
        [On][Off]
     */
    public static void TwoSidedToggleButton(ref bool value, string descLeft, string descRight)
    {
        EditorGUILayout.BeginHorizontal();

        value = GUILayout.Toggle(value, descLeft, GUI.skin.button);
        value = GUILayout.Toggle(!value, descRight, GUI.skin.button);
        value = !value;

        EditorGUILayout.EndHorizontal();
    }

    // ===============================================================================

    public static void TwoSidedToggleButton( ref float value, string descLeft, string descRight )
    {
        bool boolvalue = value > 0.0f;
        TwoSidedToggleButton( ref boolvalue, descLeft, descRight );
        value = boolvalue ? 1.0f : 0.0f;
    }

    // ===============================================================================

    public static void SetIcon( ScriptableObject so, Texture2D texture )
    {
        var ty = typeof( EditorGUIUtility );
        var mi = ty.GetMethod( "SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static );
        mi.Invoke( null, new object[] { so, texture } );
    }
}

#endif