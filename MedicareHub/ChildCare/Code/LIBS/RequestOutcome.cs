namespace ChildCare.Code.LIBS
{
  
        public class RequestOutcome<T>
        {
            public T Data { get; set; }
            public T ResponseData { get; set; }
            public string RedirectUrl { get; set; }
            //public bool IsSuccess { get { return Data == null; } }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public string ErrorMessage { get; set; }
            public string ReturnParameter { get; set; }
     

    }
}
