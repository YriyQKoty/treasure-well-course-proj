using System;

namespace WebClient
{
    [Serializable]
    public class Response
    {
        public int id;
        public string code;
        public DateTime completionDateTime;
        public object submissionDate;
        public object essayAnswer;
        public bool recordIsActive;
    }
}