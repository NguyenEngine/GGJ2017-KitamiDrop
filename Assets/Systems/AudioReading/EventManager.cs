
// ===================================================================================
// Copyright (c) 2016, Abstraction Games B.V.
// ===================================================================================

using UnityEngine;
using System.Collections.Generic;
using System;

// ===================================================================================

public class EventManager : Singleton<EventManager>
{
    /// <summary>
    /// A generic delegate type for IAgEvent handlers.
    /// </summary>
    /// <typeparam name="T">The type of IAgEvent being handled.</typeparam>
    /// <param name="theEvent">The event being handled.</param>
    public delegate void EventDelegate<T>(T theEvent);

    /// <summary>
    /// Dicitonary that stores Lists of Delegates with a the type of the IAgEvent as a key.
    /// </summary>
    public Dictionary<Type, List<Delegate>> m_handlers = new Dictionary<Type, List<Delegate>>();

    /// <summary>
    /// Subscribes a handler to the specified event type. This handler will be called when an event of the specified type is dispatched.
    /// </summary>
    /// <typeparam name="T">The IAgEvent type to subscribe to.</typeparam>
    /// <param name="handler">The EventDelegate to subscribe.</param>
    public void Subscribe<T>(EventDelegate<T> handler) where T : class, IEvent
    {
        if (m_handlers != null)
        {
            try
            {
                Type eventType = typeof(T);
                if (!m_handlers.ContainsKey(eventType))
                {
                    m_handlers.Add(eventType, new List<Delegate>());
                }

                // Throw an exception if the handler is already registered.
                if (m_handlers[eventType].Contains(handler))
                {
                    throw new Exception("The EventDelegate handler is already registered to that event.");
                }

                // Add the handler to the list under the given event type.
                m_handlers[eventType].Add(handler);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Failed to subscribe target {0} for event type {1}. Exception: {2}.", handler.Target, typeof(T), e.Message);
            }
        }
    }

    // ===============================================================================

    /// <summary>
    /// Unsubscribes the given handler from the specified IAgEvent.
    /// </summary>
    /// <typeparam name="T">The IAgEvent type to unsubscribe from.</typeparam>
    /// <param name="handler">The EventDelegate to unsubscribe.</param>
    public void Unsubscribe<T>(EventDelegate<T> handler) where T : class, IEvent
    {
        if (m_handlers != null && m_handlers.Count > 0)
        {
            try
            {
                Type eventType = typeof(T);
                List<Delegate> handlers = new List<Delegate>();

                // Verify that the handler is subscribed to that event.
                if (m_handlers.TryGetValue(eventType, out handlers))
                {
                    // Find the handler in the list of subscribers for the event type given.
                    foreach (Delegate obj in handlers)
                    {
                        if (obj == handler as Delegate)
                        {
                            //We found the delegate. Remove it.
                            handlers.Remove(handler);
                            break;
                        }
                    }

                    //Remove the list from the dictionary if there are no more handlers subscribed to that event type.
                    if (handlers.Count == 0)
                    {
                        m_handlers.Remove(eventType);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Failed to unsubscribe target {0} for event type {1}. Exception: {2}.", handler.Target, typeof(T), e.Message);
            }
        }
    }

    // ===============================================================================

    /// <summary>
    /// Posts an event directly.
    /// </summary>
    /// <param name="eventToPost">The IAgEvent to post.</param>
    public void Post(IEvent eventToPost)
    {
        Post(eventToPost, 0.0f);
    }

    // ===============================================================================

    /// <summary>
    /// Queues an event that will be posted once its timestamp is reached.
    /// </summary>
    /// <param name="eventToPost">The IAgEvent to post.</param>
    /// <param name="secondsUntilPost">The delay before posting in seconds.</param>
    public void Post(IEvent eventToPost, float secondsUntilPost)
    {
        StartCoroutine(_postEvent(eventToPost, secondsUntilPost));
    }

    // ===============================================================================

    public void PostImmediately(IEvent eventToPost)
    {
        if (eventToPost != null)
        {
            _dispatchEvent(eventToPost);
        }
    }

    // ===============================================================================

    /// <summary>
    /// The IEnumerator to handle the event and its delay via yield statements.
    /// </summary>
    /// <param name="eventToPost">The IAgEvent to post.</param>
    /// <param name="secondsUntilPost">The delay before posting in seconds.</param>
    /// <returns></returns>
    private IEnumerator<YieldInstruction> _postEvent(IEvent eventToPost, float secondsUntilPost)
    {
        if (eventToPost == null)
        {
            yield return null;
        }

        eventToPost.PostTimeStamp = Time.time;
        yield return new WaitForSeconds(secondsUntilPost);
        eventToPost.DispatchTimeStamp = Time.time;
        _dispatchEvent(eventToPost);
    }

    // ===============================================================================

    /// <summary>
    /// Dispatches an event and calls all subscribed handlers for this event.
    /// </summary>
    /// <param name="eventToDispatch">The IAgEvent to dispatch.</param>
    private void _dispatchEvent(IEvent eventToDispatch)
    {
        Type eventType = eventToDispatch.GetType();

        if (m_handlers != null && m_handlers.Count > 0)
        {
            try
            {
                // Create holder variable.
                List<Delegate> handlers = new List<Delegate>();

                // Check if we have any subscribers for this type of event.
                if (m_handlers.TryGetValue(eventType, out handlers))
                {
                    // Get the list of subscribers to this event type and invoke the event handlers.
                    foreach (Delegate obj in handlers)
                    {
                        obj.DynamicInvoke(eventToDispatch);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Failed to post event {0}. Exception: {1}. StackTrace: {2}", eventToDispatch, e.InnerException.Message, e.InnerException.StackTrace);
            }
        }
    }
}