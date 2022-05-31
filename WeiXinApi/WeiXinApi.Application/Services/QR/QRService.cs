using Furion.RemoteRequest;
using Mapster;

namespace WeiXinApi.Application.Services
{
    public class QRService : BaseService
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("qr/add")]
        public async Task<dynamic> Add(QRInput input)
        {
            var qrCode = input.Adapt<QrCode>();

            var ticks = SystemTime.Now.Ticks.ToString();
            var sceneId = int.Parse(ticks.Substring(ticks.Length - 7, 7));
            var qrResult = await QrCodeApi.CreateAsync(AppId, input.ExpireSeconds, sceneId, input.ActionName);
            var qrCodeUrl = QrCodeApi.GetShowQrCodeUrl(qrResult.ticket);
            qrCode.Ticket = qrResult.ticket;
            qrCode.CodeUrl = qrCodeUrl;
            qrCode.SceneId = sceneId;
            qrCode.CreatedTime = DateTime.Now;
            if (input.ActionName == QrCode_ActionName.QR_SCENE)
            {
                qrCode.ExpiredTime = qrCode.CreatedTime.AddSeconds(input.ExpireSeconds);
            }
            await DbContext.Db.Insertable(qrCode).ExecuteCommandAsync();//插入数据库
            return qrCodeUrl;
        }

        /// <summary>
        /// 二维码列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("qr/page")]
        public async Task<dynamic> Page(int pageNumber = 1, int pageSize = 20)
        {
            var result = await DbContext.Db.Queryable<QrCode>()
                .Mapper(it =>
                {
                    if (it.ActionName == QrCode_ActionName.QR_SCENE)//判断是否过期
                    {
                        it.IsExpired = it.ExpiredTime < DateTime.Now;
                    }

                })
                .ToPageListAsync(pageNumber, pageSize);
            return result;
        }
    }
}
