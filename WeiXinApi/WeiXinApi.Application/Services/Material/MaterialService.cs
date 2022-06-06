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
        /// 上传永久素材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/upload/forever")]
        public async Task<dynamic> UploadForever([FromForm] ForeverAddInput input)
        {

            var file = await GetPath(input.File);
            var uploadResult = await MediaApi.UploadForeverMediaAsync(AppId, file, input.UploadForeverMediaType.Value);
            return uploadResult;
        }

        /// <summary>
        /// 上传永久视频素材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/upload/video")]
        public async Task<dynamic> UploadForeverVideo([FromForm] ForeverAddInput input)
        {

            var file = await GetPath(input.File);
            var uploadResult = await MediaApi.UploadForeverVideoAsync(AppId, file, input.Title, input.Introduction);
            return uploadResult;
        }



        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("/upload/image")]
        public async Task<dynamic> UploadImage(IFormFile formFile)
        {
            if (formFile != null)
            {
                var file = await GetPath(formFile);
                var uploadResult = await MediaApi.UploadImgAsync(AppId, file);
                return uploadResult;
            }
            else
            {
                throw Oops.Oh($"文件不存在");
            }
        }


        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        [HttpPost("/upload/delete/{mediaId}")]
        public async Task<dynamic> Delete(string mediaId)
        {
            var result = await MediaApi.DeleteForeverMediaAsync(AppId, mediaId);
            return result;
        }

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/material/temp/get")]
        public async Task<dynamic> GetTemp(string mediaId)
        {
            var result = await MediaApi.GetAsync(AppId, mediaId, App.HostEnvironment.ContentRootPath);
            return result;
        }

        /// <summary>
        /// 素材总数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/material/total")]
        public async Task<dynamic> Total()
        {
            var result = await MediaApi.GetMediaCountAsync(AppId);
            return result;
        }



        /// <summary>
        /// 图文素材列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/material/imagepage")]
        public async Task<dynamic> ImagePage([FromQuery] ForeverPageInput input)
        {
            var result = await MediaApi.GetNewsMediaListAsync(AppId, input.Offset, input.Count);
            return result;
        }

        /// <summary>
        /// 素材列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/material/page")]
        public async Task<dynamic> Page([FromQuery] ForeverPageInput input)
        {
            var result = await MediaApi.GetOthersMediaListAsync(AppId, input.UploadMediaFileType, input.Offset, input.Count);
            return result;
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
