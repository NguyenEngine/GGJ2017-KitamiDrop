
using System;

// ===================================================================================

public class EventBase : IEvent 
{
    protected Guid m_id;

    public EventBase()
    {
        m_id = Guid.NewGuid();
    }

    /// <summary>
    /// Name of the event.
    /// </summary>
    public string Name
    {
        get { return GetType().ToString(); }
    }

    /// <summary>
    /// Unique identifier given to this event. Can be used for tracking purposes.
    /// </summary>
    public Guid ID
    {
        get { return m_id; }
    }

    /// <summary>
    /// Overriden for formatting purposes.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string formattedString = string.Format( "[EventName: {0} - EventID: {1}]", Name, ID );
        return formattedString;
    }

    /// <summary>
    /// Timestamp given to this event when it was posted.
    /// </summary>
    public float PostTimeStamp
    {
        get;
        set;
    }

    /// <summary>
    /// Timestamp given to this event when it was dispatched.
    /// This can differ from the posting timestamp when called with a delay.
    /// </summary>
    public float DispatchTimeStamp
    {
        get;
        set;
    }
}