namespace Assignment.NotificationType
{
	[Flags]
    enum NotificationTypes : uint
    {
        SMS		= 1 << 0,
        Email	= 1 << 1,
        Push	= 1 << 2,

		All		= ~0u
    }
}
