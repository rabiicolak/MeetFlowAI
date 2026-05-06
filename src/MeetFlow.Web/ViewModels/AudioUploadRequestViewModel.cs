using Microsoft.AspNetCore.Http;

namespace MeetFlow.Web.ViewModels
{
    public class AudioUploadRequestViewModel
    {
        public IFormFile? AudioFile { get; set; }
    }
}
