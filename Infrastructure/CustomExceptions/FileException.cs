using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomExceptions
{
    [Serializable]
    public class FileException:BaseException
    {
        public const string OverLengthException = "Fayl ölçüsü 5 MB-dan çox ola bilməz!";
        public const string NotCorrectExtensionException = "Fayl .pdf və ya .docx formatında olmalıdır!";
        public const string UploadFileException = "Fayl yüklənən zaman xəta yarandı!";
        public FileException()  { }
        public FileException(string? message) : base(message) { }
        public FileException(string? message, Exception? innerException) : base(message, innerException) { }
        public FileException(int statusCode, string? message) : base(statusCode, message) { }
        public FileException(int statusCode, string? message, Exception? innerException) : base(statusCode, message, innerException) { }
        protected FileException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
