using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Controllers
{
    [AllowAnonymous]
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BankNumber()
        {
            return View();
        }

        [HttpGet]        
        public ActionResult CheckBank(string number)
        {
            //6217000010101815879
            //6214686002098863
            //6228480018824629879
            try
            {
                string url = string.Format("https://ccdcapi.alipay.com/validateAndCacheCardInfo.json?_input_charset=utf-8&cardNo={0}&cardBinCheck=true", number);
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "Get";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                WebResponse response = request.GetResponse();
                var stream = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    var content = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var model = JsonConvert.DeserializeObject<BankNoModel>(content);
                        if (model.stat == "ok" && model.validated)
                        {
                            if (bankCodeDic().ContainsKey(model.bank))
                                model.bankName = bankCodeDic()[model.bank];

                            HttpWebRequest request2 = HttpWebRequest.Create("https://apimg.alipay.com/combo.png?d=cashier&t=" + model.bank) as HttpWebRequest;
                            request2.Method = "Get";
                            request2.ContentType = "image/jpeg";
                            WebResponse response2 = request2.GetResponse();

                            Image img = Image.FromStream(response2.GetResponseStream());

                            MemoryStream ms = new MemoryStream();
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            byte[] arr = new byte[ms.Length];
                            ms.Position = 0;
                            ms.Read(arr, 0, (int)ms.Length);
                            ms.Close();
                            string base64 = Convert.ToBase64String(arr);

                            model.bankLogo = "data:image/jpeg;base64," + base64;

                        }
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                }
                return Content("");
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        public class BankNoModel
        {
            public string stat { get; set; }
            public string bank { get; set; }
            public bool validated { get; set; }
            public string cardType { get; set; }
            public string key { get; set; }
            public List<Dictionary<string, string>> messages { get; set; }

            public string bankName { get; set; }
            public string bankLogo { get; set; }

        }

        private Dictionary<string, string> bankCodeDic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("SRCB", "深圳农村商业银行");
            dic.Add("BGB", "广西北部湾银行");
            dic.Add("SHRCB", "上海农村商业银行");
            dic.Add("BJBANK", "北京银行");
            dic.Add("WHCCB", "威海市商业银行");
            dic.Add("BOZK", "周口银行");
            dic.Add("KORLABANK", "库尔勒市商业银行");
            dic.Add("SPABANK", "平安银行");
            dic.Add("SDEB", "顺德农商银行");
            dic.Add("HURCB", "湖北省农村信用社");
            dic.Add("WRCB", "无锡农村商业银行");
            dic.Add("BOCY", "朝阳银行");
            dic.Add("CZBANK", "浙商银行");
            dic.Add("HDBANK", "邯郸银行");
            dic.Add("BOC", "中国银行");
            dic.Add("BOD", "东莞银行");
            dic.Add("CCB", "中国建设银行");
            dic.Add("ZYCBANK", "遵义市商业银行");
            dic.Add("SXCB", "绍兴银行");
            dic.Add("GZRCU", "贵州省农村信用社");
            dic.Add("ZJKCCB", "张家口市商业银行");
            dic.Add("BOJZ", "锦州银行");
            dic.Add("BOP", "平顶山银行");
            dic.Add("HKB", "汉口银行");
            dic.Add("SPDB", "上海浦东发展银行");
            dic.Add("NXRCU", "宁夏黄河农村商业银行");
            dic.Add("NYNB", "广东南粤银行");
            dic.Add("GRCB", "广州农商银行");
            dic.Add("BOSZ", "苏州银行");
            dic.Add("HZCB", "杭州银行");
            dic.Add("HSBK", "衡水银行");
            dic.Add("HBC", "湖北银行");
            dic.Add("JXBANK", "嘉兴银行");
            dic.Add("HRXJB", "华融湘江银行");
            dic.Add("BODD", "丹东银行");
            dic.Add("AYCB", "安阳银行");
            dic.Add("EGBANK", "恒丰银行");
            dic.Add("CDB", "国家开发银行");
            dic.Add("TCRCB", "江苏太仓农村商业银行");
            dic.Add("NJCB", "南京银行");
            dic.Add("ZZBANK", "郑州银行");
            dic.Add("DYCB", "德阳商业银行");
            dic.Add("YBCCB", "宜宾市商业银行");
            dic.Add("SCRCU", "四川省农村信用");
            dic.Add("KLB", "昆仑银行");
            dic.Add("LSBANK", "莱商银行");
            dic.Add("YDRCB", "尧都农商行");
            dic.Add("CCQTGB", "重庆三峡银行");
            dic.Add("FDB", "富滇银行");
            dic.Add("JSRCU", "江苏省农村信用联合社");
            dic.Add("JNBANK", "济宁银行");
            dic.Add("CMB", "招商银行");
            dic.Add("JINCHB", "晋城银行JCBANK");
            dic.Add("FXCB", "阜新银行");
            dic.Add("WHRCB", "武汉农村商业银行");
            dic.Add("HBYCBANK", "湖北银行宜昌分行");
            dic.Add("TZCB", "台州银行");
            dic.Add("TACCB", "泰安市商业银行");
            dic.Add("XCYH", "许昌银行");
            dic.Add("CEB", "中国光大银行");
            dic.Add("NXBANK", "宁夏银行");
            dic.Add("HSBANK", "徽商银行");
            dic.Add("JJBANK", "九江银行");
            dic.Add("NHQS", "农信银清算中心");
            dic.Add("MTBANK", "浙江民泰商业银行");
            dic.Add("LANGFB", "廊坊银行");
            dic.Add("ASCB", "鞍山银行");
            dic.Add("KSRB", "昆山农村商业银行");
            dic.Add("YXCCB", "玉溪市商业银行");
            dic.Add("DLB", "大连银行");
            dic.Add("DRCBCL", "东莞农村商业银行");
            dic.Add("GCB", "广州银行");
            dic.Add("NBBANK", "宁波银行");
            dic.Add("BOYK", "营口银行");
            dic.Add("SXRCCU", "陕西信合");
            dic.Add("GLBANK", "桂林银行");
            dic.Add("BOQH", "青海银行");
            dic.Add("CDRCB", "成都农商银行");
            dic.Add("QDCCB", "青岛银行");
            dic.Add("HKBEA", "东亚银行");
            dic.Add("HBHSBANK", "湖北银行黄石分行");
            dic.Add("WZCB", "温州银行");
            dic.Add("TRCB", "天津农商银行");
            dic.Add("QLBANK", "齐鲁银行");
            dic.Add("GDRCC", "广东省农村信用社联合社");
            dic.Add("ZJTLCB", "浙江泰隆商业银行");
            dic.Add("GZB", "赣州银行");
            dic.Add("GYCB", "贵阳市商业银行");
            dic.Add("CQBANK", "重庆银行");
            dic.Add("DAQINGB", "龙江银行");
            dic.Add("CGNB", "南充市商业银行");
            dic.Add("SCCB", "三门峡银行");
            dic.Add("CSRCB", "常熟农村商业银行");
            dic.Add("SHBANK", "上海银行");
            dic.Add("JLBANK", "吉林银行");
            dic.Add("CZRCB", "常州农村信用联社");
            dic.Add("BANKWF", "潍坊银行");
            dic.Add("ZRCBANK", "张家港农村商业银行");
            dic.Add("FJHXBC", "福建海峡银行");
            dic.Add("ZJNX", "浙江省农村信用社联合社");
            dic.Add("LZYH", "兰州银行");
            dic.Add("JSB", "晋商银行");
            dic.Add("BOHAIB", "渤海银行");
            dic.Add("CZCB", "浙江稠州商业银行");
            dic.Add("YQCCB", "阳泉银行");
            dic.Add("SJBANK", "盛京银行");
            dic.Add("XABANK", "西安银行");
            dic.Add("BSB", "包商银行");
            dic.Add("JSBANK", "江苏银行");
            dic.Add("FSCB", "抚顺银行");
            dic.Add("HNRCU", "河南省农村信用");
            dic.Add("COMM", "交通银行");
            dic.Add("XTB", "邢台银行");
            dic.Add("CITIC", "中信银行");
            dic.Add("HXBANK", "华夏银行");
            dic.Add("HNRCC", "湖南省农村信用社");
            dic.Add("DYCCB", "东营市商业银行");
            dic.Add("ORBANK", "鄂尔多斯银行");
            dic.Add("BJRCB", "北京农村商业银行");
            dic.Add("XYBANK", "信阳银行");
            dic.Add("ZGCCB", "自贡市商业银行");
            dic.Add("CDCB", "成都银行");
            dic.Add("HANABANK", "韩亚银行");
            dic.Add("CMBC", "中国民生银行");
            dic.Add("LYBANK", "洛阳银行");
            dic.Add("GDB", "广东发展银行");
            dic.Add("ZBCB", "齐商银行");
            dic.Add("CBKF", "开封市商业银行");
            dic.Add("H3CB", "内蒙古银行");
            dic.Add("CIB", "兴业银行");
            dic.Add("CRCBANK", "重庆农村商业银行");
            dic.Add("SZSBK", "石嘴山银行");
            dic.Add("DZBANK", "德州银行");
            dic.Add("SRBANK", "上饶银行");
            dic.Add("LSCCB", "乐山市商业银行");
            dic.Add("JXRCU", "江西省农村信用");
            dic.Add("ICBC", "中国工商银行");
            dic.Add("JZBANK", "晋中市商业银行");
            dic.Add("HZCCB", "湖州市商业银行");
            dic.Add("NHB", "南海农村信用联社");
            dic.Add("XXBANK", "新乡银行");
            dic.Add("JRCB", "江苏江阴农村商业银行");
            dic.Add("YNRCC", "云南省农村信用社");
            dic.Add("ABC", "中国农业银行");
            dic.Add("GXRCU", "广西省农村信用");
            dic.Add("PSBC", "中国邮政储蓄银行");
            dic.Add("BZMD", "驻马店银行");
            dic.Add("ARCU", "安徽省农村信用社");
            dic.Add("GSRCU", "甘肃省农村信用");
            dic.Add("LYCB", "辽阳市商业银行");
            dic.Add("JLRCU", "吉林农信");
            dic.Add("URMQCCB", "乌鲁木齐市商业银行");
            dic.Add("XLBANK", "中山小榄村镇银行");
            dic.Add("CSCB", "长沙银行");
            dic.Add("JHBANK", "金华银行");
            dic.Add("BHB", "河北银行");
            dic.Add("NBYZ", "鄞州银行");
            dic.Add("LSBC", "临商银行");
            dic.Add("BOCD", "承德银行");
            dic.Add("SDRCU", "山东农信");
            dic.Add("NCB", "南昌银行");
            dic.Add("TCCB", "天津银行");
            dic.Add("WJRCB", "吴江农商银行");
            dic.Add("CBBQS", "城市商业银行资金清算中心");
            dic.Add("HBRCU", "河北省农村信用社");

            return dic;
        }
    }
}