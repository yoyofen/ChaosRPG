namespace ShooterRPG
{
    using System;

    public class ContentLoadException : Exception
    {
        public ContentLoadException()
            : this("Content loading failed")
        {

        }

        public ContentLoadException(string msg)
            : base(msg)
        {

        }
    }
}
