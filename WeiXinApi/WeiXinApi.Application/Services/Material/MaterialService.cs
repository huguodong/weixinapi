namespace WeiXinApi.Application.Services
{
    public class MaterialService : BaseService
    {

        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/upload/temp")]
        public async Task<dynamic> UploadTemp([FromForm] MaterialAddInput input)
        {

            var file = await GetPath(input.File);
            var uploadResult = await MediaApi.UploadTemporaryMediaAsync(AppId, input.UploadMediaFileType.Value, file, 50000);
            return uploadResult.media_id;
        }

        /// <summary>
        /// 保存文件到本地
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isTemp"></param>
        /// <returns></returns>
        [NonAction]//不是API
        private async Task<string> GetPath(IFormFile file, bool isTemp = true)
        {
            var dir = isTemp ? "temp" : "permanent";
            // 保存到网站根目录下的 uploads 目录
            var savePath = Path.Combine(App.HostEnvironment.ContentRootPath, "uploads", dir);
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            // 避免文件名重复，采用 GUID 生成
            var filePath = Path.Combine(savePath, Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName));  // 可以替代为你需要存储的真实路径
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
