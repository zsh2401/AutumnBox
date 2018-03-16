namespace AutumnBox.OpenFramework.Internal
{
    public static class ContextFactory
    {
        private class HighPermissionContext : Context
        {
            public override string Tag => base.Tag;
            public Context SourceContext { get; private set; }
            public HighPermissionContext(Context lowPermissionContext)
            {
                SourceContext = lowPermissionContext;
            }
            internal override ContextPermissionLevel GetPermissionLevel()
            {
                return ContextPermissionLevel.High;
            }
        }
        public static Context GetHighPermissionContext(Context context)
        {
            context.PermissionCheck(ContextPermissionLevel.Mid);
            return new HighPermissionContext(context);
        }
    }
}
