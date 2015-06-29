using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complex.Common.Config
{
    /// <summary>
    /// 此地区表对应游戏那边的地区表数据。
    /// </summary>
    public class Regin
    {

        //private Dictionary<string, string>[] citys = new Dictionary<string, string>[33];
        private static Dictionary<string, string> city = new Dictionary<string, string>();

        /// <summary>
        /// 所有市区的集合
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> Regins = new Dictionary<string, Dictionary<string, string>>(33);
        /// <summary>
        /// 所有城市的集合
        /// </summary>
        public static Dictionary<string, string> Provinces = new Dictionary<string, string>(33);

        /// <summary>
        /// 根据code获取地区的文本：
        /// 当all为true时,返回：省份 城市
        /// 反之返回：城市
        /// </summary>
        /// <param name="code"></param>
        /// <param name="all">true：返回：城市；false：返回:省份 城市</param>
        /// <returns></returns>
        public static string GetTextByCode(int code, bool all = false)
        {
            if (code < 100)
            {
                if (code == 0)
                {
                    if (all)
                    {
                        return Provinces["0"] + " " + Regins["0"]["0"];
                    }
                    else
                    {
                        return Provinces["0"];
                    }
                }
                else
                {
                    return pszSheng[code];
                }
            }
            else
            {
                var provinceCode = code / 100;
                var cityCode = code % 100;

                if (all)
                {
                    try
                    {
                        return Provinces[provinceCode.ToString()] + " " + Regins[provinceCode.ToString()][cityCode.ToString()];
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    try
                    {
                        return Regins[provinceCode.ToString()][cityCode.ToString()];
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }



        private static string[] pszSheng = new string[]{"北京","上海","天津","重庆","黑龙江","吉林","辽宁","山东",
                            "山西","陕西","河北","河南","湖北","湖南","海南","江苏","江西","广东",
                            "广西","云南","贵州","四川","内蒙古","宁夏","甘肃","青海","安徽","浙江",
                            "福建","西藏","台湾","香港","澳门"};




        static Regin()
        {

            for (int i = 0; i < pszSheng.Length; i++)
            {
                Provinces.Add(i.ToString(), pszSheng[i]);
            }



            //以上为省份   省份= new string[]0~32


            //北京
            string[] pszBeiJing = new string[]{"东城区","西城区","崇文区","宣武区","朝阳区","丰台区","石景山区","海淀区",
"门头沟区","房山区","通州区","顺义区","昌平区","大兴区","怀柔区","平谷区","密云县","延庆县","0"};

            //上海
            string[] pszShangHai = new string[]{"黄浦区","卢湾区","徐汇区","长宁区","静安区","普陀区","闸北区","宝山区",
"嘉定区","浦东新区","金山区","松江区","青浦区","南汇区","奉贤区","崇明县","0"};
            //天津
            string[] pszTianJin = new string[]{"和平区","河东区","河西区","南开区","河北区","红桥区","塘沽区","汉沽区",
"大港区","东丽区","西青区","津南区","北辰区","武清区","宝坻区","宁河县","静海县","蓟县","0"};
            //重庆
            string[] pszChongQing = new string[]{"万州区","涪陵区","渝中区","大渡口区","江北区","九龙坡区","南岸区","北碚区",
"万盛区","双桥区","渝北区","巴南区","黔江区","长寿区","江津区","合川区","永川区","南川区",
"綦江县","铜梁县","大足县","荣昌县","璧山县","梁平县","城口县","丰都县","垫江县","武隆县",
"忠县","开县","云阳县","奉节县","巫山县","石柱土家族自治县","秀山土家族苗族自治县","酉阳土家族苗族自治县","彭水苗族土家族自治县","0"};
            //黑龙江
            string[] pszHeiLongJiang = new string[]{"哈尔滨市","齐齐哈尔市","鸡西市","鹤岗市","双鸭山市","大庆市","伊春市","佳木斯市","七台河市",
"牡丹江市","黑河市","绥化市","大兴安岭地区","0"};
            //吉林
            string[] pszJiLin = new string[] { "长春市", "吉林市", "四平市", "辽源市", "通化市", "白山市", "松原市", "白城市", "延边朝鲜族自治州", "0" };
            //辽宁
            string[] pszLiaoNing = new string[]{"沈阳市","大连市","鞍山市","抚顺市","本溪市","丹东市","锦州市","营口市","阜新市","辽阳市",
"盘锦市","铁岭市","朝阳市","葫芦岛市","0"};
            //山东
            string[] pszShanDong = new string[]{"济南市","青岛市","淄博市","枣庄市","东营市","烟台市","潍坊市","济宁市","泰安市","威海市",
"日照市","莱芜市","临沂市","德州市","聊城市","滨州市","菏泽市","0"};
            //山西
            string[] pszShanXi = new string[]{"太原市","大同市","阳泉市","长治市","晋城市","朔州市","晋中市","运城市","忻州市","临汾市",
"吕梁市","0"};
            //陕西
            string[] pszShanXi1 = new string[] { "西安市", "铜川市", "宝鸡市", "咸阳市", "渭南市", "延安市", "汉中市", "榆林市", "安康市", "商洛市", "0" };
            //河北
            string[] pszHeBei = new string[]{"石家庄市","唐山市","秦皇岛市","邯郸市","邢台市","保定市","张家口市","承德市","沧州市","廊坊市",
"衡水市","0"};
            //河南
            string[] pszHeNan = new string[]{"郑州市","开封市","洛阳市","平顶山市","安阳市","鹤壁市","新乡市","焦作市","济源市","濮阳市",
"许昌市","漯河市","三门峡市","南阳市","商丘市","信阳市","周口市","驻马店市","0"};
            //湖北
            string[] pszHuBei = new string[]{"武汉市","黄石市","十堰市","宜昌市","襄樊市","鄂州市","荆门市","孝感市","荆州市","黄冈市",
"咸宁市","随州市","恩施土家族苗族自治州","仙桃市","潜江市","天门市","神农架林区","0"};
            //湖南
            string[] pszHuNan = new string[]{"长沙市","株洲市","湘潭市","衡阳市","邵阳市","岳阳市","常德市","张家界市","益阳市","郴州市",
"永州市","怀化市","娄底市","湘西土家族苗族自治州","0"};
            //海南
            string[] pszHaiNan = new string[]{"海口市","三亚市","五指山市","琼海市","儋州市","文昌市","万宁市","东方市","定安县","屯昌县",
"澄迈县","临高县","白沙黎族自治县","昌江黎族自治县","乐东黎族自治县","陵水黎族自治县","保亭黎族苗族自治县","琼中黎族苗族自治县","0"};
            //江苏
            string[] pszJiangSu = new string[]{"南京市","无锡市","徐州市","常州市","苏州市","南通市","连云港市","淮安市","盐城市","扬州市",
"镇江市","泰州市","宿迁市","0"};
            //江西
            string[] pszJiangXi = new string[]{"南昌市","景德镇市","萍乡市","九江市","新余市","鹰潭市","赣州市","吉安市","宜春市","抚州市",
"上饶市","0"};
            //广东
            string[] pszGuangDong = new string[]{"广州市","韶关市","深圳市","珠海市","汕头市","佛山市","江门市","湛江市","茂名市","肇庆市",
"惠州市","梅州市","汕尾市","河源市","阳江市","清远市","东莞市","中山市","潮州市","揭阳市","0"};
            //广西
            string[] pszGuangXi = new string[]{"南宁市","柳州市","桂林市","梧州市","北海市","防城港市","钦州市","贵港市","玉林市","百色市",
"贺州市","河池市","来宾市","崇左市","0"};
            //云南
            string[] pszYunNan = new string[]{"昆明市","曲靖市","玉溪市","保山市","昭通市","丽江市","思茅市","临沧市","楚雄彝族自治州",
"红河哈尼族彝族自治州","文山壮族苗族自治州","西双版纳傣族自治州","大理白族自治州","德宏傣族景颇族自治州","怒江傈僳族自治州","迪庆藏族自治州","0"};
            //贵州
            string[] pszGuiZhou = new string[]{"贵阳市","六盘水市","遵义市","安顺市","铜仁地区","黔西南布依族苗族自治州","毕节地区","黔东南苗族侗族自治州",
"黔南布依族苗族自治州","0"};
            //四川
            string[] pszSiChuan = new string[]{"成都市","自贡市","攀枝花市","泸州市","德阳市","绵阳市","广元市","遂宁市","内江市","乐山市","南充市",
"眉山市","宜宾市","广安市","达州市","雅安市","巴中市","资阳市","阿坝藏族羌族自治州","甘孜藏族自治州","凉山彝族自治州","0"};
            //内蒙古
            string[] pszNeiMeng = new string[]{"呼和浩特市","包头市","乌海市","赤峰市","通辽市","鄂尔多斯市","呼伦贝尔市","巴彦淖尔市","乌兰察布市",
"兴安盟","锡林郭勒盟","阿拉善盟","0"};
            //宁夏
            string[] pszNingXia = new string[] { "银川市", "石嘴山市", "吴忠市", "固原市", "中卫市", "0" };
            //甘肃
            string[] pszGanSu = new string[]{"兰州市","嘉峪关市","金昌市","白银市","天水市","武威市","张掖市","平凉市","酒泉市","庆阳市","定西市",
"陇南市","临夏回族自治州","甘南藏族自治州","0"};
            //青海
            string[] pszQingHai = new string[]{"西宁市","海东地区","海北藏族自治州","黄南藏族自治州","海南藏族自治州","果洛藏族自治州","玉树藏族自治州",
"海西蒙古族藏族自治州","0"};
            //安徽
            string[] pszAnHui = new string[]{"合肥市","芜湖市","蚌埠市","淮南市","马鞍山市","淮北市","铜陵市","安庆市","黄山市","滁州市","阜阳市",
"宿州市","巢湖市","六安市","亳州市","池州市","宣城市","0"};
            //浙江
            string[] pszZheJiang = new string[] { "金华市", "杭州市", "宁波市", "温州市", "嘉兴市", "湖州市", "绍兴市", "衢州市", "舟山市", "台州市", "丽水市", "0" };
            //福建
            string[] pszFuJian = new string[] { "福州市", "厦门市", "莆田市", "三明市", "泉州市", "漳州市", "南平市", "龙岩市", "宁德市", "0" };
            //西藏
            string[] pszXiZang = new string[]{"拉萨市","昌都地区","山南地区新疆","乌鲁木齐市","克拉玛依市","吐鲁番地区","哈密地区日喀则地区","那曲地区","阿里地区",
"林芝地区","昌吉回族自治州","博尔塔拉蒙古自治州","巴音郭楞蒙古自治州","阿克苏地区","克孜勒苏柯尔克孜自治州","喀什地区","和田地区",
"伊犁哈萨克自治州","塔城地区","阿勒泰地区","石河子市","阿拉尔市","图木舒克市","五家渠市","0"};
            //台湾
            string[] pszTaiWan = new string[] { "台北市", "高雄市", "基隆市", "台中市", "台南市", "新竹市", "嘉义市", "0" };
            //香港
            string[] pszXiangGang = new string[]{"中西区","湾仔区","东区","南区","油尖旺区","深水埗区","九龙城区","黄大仙区","观塘区","荃湾区","葵青区",
"沙田区","西贡区","大埔区","北区","元朗区","屯门区","离岛区","0"};
            //澳门
            string[] pszAoMen = new string[] { "澳门", "0" };


            //以上为市 市

            //城市代码为 省*100+市




            #region MyRegion

            //北京       
            for (int i = 0; i < pszBeiJing.Length; i++)
            {
                city.Add(i.ToString(), pszBeiJing[i]);
            }
            Regins.Add("0", new Dictionary<string, string>(city));
            city.Clear();
            //上海

            for (int i = 0; i < pszShangHai.Length; i++)
            {
                city.Add((1 * 100 + i).ToString(), pszShangHai[i]);
            }
            Regins.Add("1", new Dictionary<string, string>(city));
            city.Clear();

            //天津
            for (int i = 0; i < pszTianJin.Length; i++)
            {
                city.Add((2 * 100 + i).ToString(), pszTianJin[i]);
            }
            Regins.Add("2", new Dictionary<string, string>(city));
            city.Clear();

            //重庆
            for (int i = 0; i < pszChongQing.Length; i++)
            {
                city.Add((3 * 100 + i).ToString(), pszChongQing[i]);
            }
            Regins.Add("3", new Dictionary<string, string>(city));
            city.Clear();

            //黑龙江
            for (int i = 0; i < pszHeiLongJiang.Length; i++)
            {
                city.Add((4 * 100 + i).ToString(), pszHeiLongJiang[i]);
            }
            Regins.Add("4", new Dictionary<string, string>(city));
            city.Clear();

            //吉林

            for (int i = 0; i < pszJiLin.Length; i++)
            {
                city.Add((5 * 100 + i).ToString(), pszJiLin[i]);
            }
            Regins.Add("5", new Dictionary<string, string>(city));
            city.Clear();

            //辽宁
            for (int i = 0; i < pszLiaoNing.Length; i++)
            {
                city.Add((6 * 100 + i).ToString(), pszLiaoNing[i]);
            }
            Regins.Add("6", new Dictionary<string, string>(city));
            city.Clear();

            //山东

            for (int i = 0; i < pszShanDong.Length; i++)
            {
                city.Add((7 * 100 + i).ToString(), pszShanDong[i]);
            }
            Regins.Add("7", new Dictionary<string, string>(city));
            city.Clear();

            //山西

            for (int i = 0; i < pszShanXi.Length; i++)
            {
                city.Add((8 * 100 + i).ToString(), pszShanXi[i]);
            }
            Regins.Add("8", new Dictionary<string, string>(city));
            city.Clear();

            //陕西

            for (int i = 0; i < pszShanXi1.Length; i++)
            {
                city.Add((9 * 100 + i).ToString(), pszShanXi1[i]);
            }
            Regins.Add("9", new Dictionary<string, string>(city));
            city.Clear();

            //河北

            for (int i = 0; i < pszHeBei.Length; i++)
            {
                city.Add((10 * 100 + i).ToString(), pszHeBei[i]);
            }
            Regins.Add("10", new Dictionary<string, string>(city));
            city.Clear();

            //河南

            for (int i = 0; i < pszHeNan.Length; i++)
            {
                city.Add((11 * 100 + i).ToString(), pszHeNan[i]);
            }
            Regins.Add("11", new Dictionary<string, string>(city));
            city.Clear();

            //湖北

            for (int i = 0; i < pszHuBei.Length; i++)
            {
                city.Add((12 * 100 + i).ToString(), pszHuBei[i]);
            }
            Regins.Add("12", new Dictionary<string, string>(city));
            city.Clear();

            //湖南

            for (int i = 0; i < pszHuNan.Length; i++)
            {
                city.Add((13 * 100 + i).ToString(), pszHuNan[i]);
            }
            Regins.Add("13", new Dictionary<string, string>(city));
            city.Clear();

            //海南

            for (int i = 0; i < pszHaiNan.Length; i++)
            {
                city.Add((14 * 100 + i).ToString(), pszHaiNan[i]);
            }
            Regins.Add("14", new Dictionary<string, string>(city));
            city.Clear();

            //江苏

            for (int i = 0; i < pszJiangSu.Length; i++)
            {
                city.Add((15 * 100 + i).ToString(), pszJiangSu[i]);
            }
            Regins.Add("15", new Dictionary<string, string>(city));
            city.Clear();

            //江西

            for (int i = 0; i < pszJiangXi.Length; i++)
            {
                city.Add((16 * 100 + i).ToString(), pszJiangXi[i]);
            }
            Regins.Add("16", new Dictionary<string, string>(city));
            city.Clear();

            //广东

            for (int i = 0; i < pszGuangDong.Length; i++)
            {
                city.Add((17 * 100 + i).ToString(), pszGuangDong[i]);
            }
            Regins.Add("17", new Dictionary<string, string>(city));
            city.Clear();

            //广西

            for (int i = 0; i < pszGuangXi.Length; i++)
            {
                city.Add((18 * 100 + i).ToString(), pszGuangXi[i]);
            }
            Regins.Add("18", new Dictionary<string, string>(city));
            city.Clear();

            //云南

            for (int i = 0; i < pszYunNan.Length; i++)
            {
                city.Add((19 * 100 + i).ToString(), pszYunNan[i]);
            }
            Regins.Add("19", new Dictionary<string, string>(city));
            city.Clear();

            //贵州

            for (int i = 0; i < pszGuiZhou.Length; i++)
            {
                city.Add((20 * 100 + i).ToString(), pszGuiZhou[i]);
            }
            Regins.Add("20", new Dictionary<string, string>(city));
            city.Clear();

            //四川

            for (int i = 0; i < pszSiChuan.Length; i++)
            {
                city.Add((21 * 100 + i).ToString(), pszSiChuan[i]);
            }
            Regins.Add("21", new Dictionary<string, string>(city));
            city.Clear();

            //内蒙古

            for (int i = 0; i < pszNeiMeng.Length; i++)
            {
                city.Add((22 * 100 + i).ToString(), pszNeiMeng[i]);
            }
            Regins.Add("22", new Dictionary<string, string>(city));
            city.Clear();


            //宁夏

            for (int i = 0; i < pszNingXia.Length; i++)
            {
                city.Add((23 * 100 + i).ToString(), pszNingXia[i]);
            }
            Regins.Add("23", new Dictionary<string, string>(city));
            city.Clear();


            //甘肃

            for (int i = 0; i < pszGanSu.Length; i++)
            {
                city.Add((24 * 100 + i).ToString(), pszGanSu[i]);
            }
            Regins.Add("24", new Dictionary<string, string>(city));
            city.Clear();


            //青海

            for (int i = 0; i < pszQingHai.Length; i++)
            {
                city.Add((25 * 100 + i).ToString(), pszQingHai[i]);
            }
            Regins.Add("25", new Dictionary<string, string>(city));
            city.Clear();


            //安徽

            for (int i = 0; i < pszAnHui.Length; i++)
            {
                city.Add((26 * 100 + i).ToString(), pszAnHui[i]);
            }
            Regins.Add("26", new Dictionary<string, string>(city));
            city.Clear();


            //浙江

            for (int i = 0; i < pszZheJiang.Length; i++)
            {
                city.Add((27 * 100 + i).ToString(), pszZheJiang[i]);
            }
            Regins.Add("27", new Dictionary<string, string>(city));
            city.Clear();


            //福建

            for (int i = 0; i < pszFuJian.Length; i++)
            {
                city.Add((28 * 100 + i).ToString(), pszFuJian[i]);
            }
            Regins.Add("28", new Dictionary<string, string>(city));
            city.Clear();


            //西藏

            for (int i = 0; i < pszXiZang.Length; i++)
            {
                city.Add((29 * 100 + i).ToString(), pszXiZang[i]);
            }
            Regins.Add("29", new Dictionary<string, string>(city));
            city.Clear();


            //台湾

            for (int i = 0; i < pszTaiWan.Length; i++)
            {
                city.Add((30 * 100 + i).ToString(), pszTaiWan[i]);
            }
            Regins.Add("30", new Dictionary<string, string>(city));
            city.Clear();

            //香港

            for (int i = 0; i < pszXiangGang.Length; i++)
            {
                city.Add((31 * 100 + i).ToString(), pszXiangGang[i]);
            }
            Regins.Add("31", new Dictionary<string, string>(city));
            city.Clear();

            //澳门

            for (int i = 0; i < pszAoMen.Length; i++)
            {
                city.Add((32 * 100 + i).ToString(), pszAoMen[i]);
            }
            Regins.Add("32", new Dictionary<string, string>(city));
            city.Clear();

            #endregion

        }



    }
}
