

namespace WeiXinApi.Application.Services
{
    public class MaterialAddInput
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        [Required(ErrorMessage = "文件类型不能为空")]
        public UploadMediaFileType? UploadMediaFileType { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [Required(ErrorMessage = "文件不存在")]
        public IFormFile File { get; set; }
    }

    public class ForeverAddInput
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        [Required(ErrorMessage = "文件类型不能为空")]
        public UploadForeverMediaType? UploadForeverMediaType { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [Required(ErrorMessage = "文件不存在")]
        public IFormFile File { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Introduction { get; set; }
    }



    public class ForeverPageInput
    {
        /// <summary>
        /// 媒体类型
        /// </summary>
        public UploadMediaFileType UploadMediaFileType { get; set; }

        /// <summary>
        /// 从全部素材的该偏移位置开始返回，0表示从第一个素材 返回
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// 返回素材的数量，取值在1到20之间
        /// </summary>
        public int Count { get; set; } = 20;
    }

}
