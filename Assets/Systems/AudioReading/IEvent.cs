

using System;

// ===================================================================================

/// <summary>
/// Interface to events the EventManager handles.
/// </summary>
public interface IEvent 
{
    /// <summary>
    /// Name of the event.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// timestamp given to this event when it was posted.
    /// </summary>
    float PostTimeStamp { get; set; }

    /// <summary>
    /// timestamp given to this event when it was dispatched.
    /// </summary>
    float DispatchTimeStamp { get; set; }

    /// <summary>
    /// Unique identifier given to this event. Can be used for tracking purposes.
    /// </summary>
    Guid ID { get; }
}