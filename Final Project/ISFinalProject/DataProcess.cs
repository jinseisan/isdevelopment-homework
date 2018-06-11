using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;

namespace ISFinalProject
{
    //数据处理类（文本处理主逻辑类）
    class DataProcess
    {
        //文件路径
        static string NLPIRAdress = @"../../ProcessingFiles/NLPIR.txt";//经过分词后的NLPIR.txt文件路径
        static string ClassifyAdress = @"../../ProcessingFiles/Classify.txt";//经过词性和构词处理后的Classify.txt文件路径
        static string LevelAdress = @"../../ProcessingFiles/Level.txt";//经过分类处理的Level.txt文件路径
        static string FSAdress = @"../../ProcessingFiles/FS.txt";//经过等级处理的FS.txt文件路径
        static string FeaturesAdress = @"../../ProcessingFiles/Features.txt";//经过fs处理最终包含所有特征的的Featurs.txt文件路径
        static string ResultAdress = @"../../ProcessingFiles/Results.txt";//经过CRFS处理得到结果的的Results.txt文件路径

        //标点符号
        static string Punctuations = "：:，,？?、.。！!“”…‘’—－《》()（）[]·<>";

        //分类特征变量
        //1.指事字 特征X
        static string word_zs = 
            @"一二三四五六七八九十上下中夫天亦杀要母介孔尺寸叉尤丑曰甘只音兮乎本末朱束金血刃凶丹引"+
            @"系卒彭卤上一二下│八小ㄔㄐ爻乃入ㄇ出回克凶文ㄙ曲/ \ㄟㄈ缀X乙己元示王中屯牟牽土喦只音尹"+
            @"父聿寸羊ㄓˋ爭刃曰甘曶ㄎ兮乎血京ㄅ一今久本末朱亙才之乇束旦毌韭耑敝卒尺兀丏欠而豖马"+
            @"馬亦夾ㄕㄢˇ夫立耴西脊毋弗弋氐或引彊勺且丑卯亥ㄩ乏廷逆世臣幻夏巾禾夕片匕殷縣县ㄒㄧㄠ司"+
            @"ㄗ抑ㄅ永派迅非不至ㄤ继繼";
        //2.象形字 特征Y
        static string word_xx = 
            "丫丰乌丹册乐了丁不丑丏业丙乙乞也主八勺勿匕卜卤卣刀龟兔儿兆兕兢于互井云亚兽几凡卯卵冉"+
            "网冏力出函人仓介以侯入升午克亢亥交亨京亭又反若蓑彳巢川大夭夫奠飞干工巨巫弓弗弟己已彘巾"+
            "帚带帝口吕向周黾马门它宫寅女山尸居壶才巳巴土堆囟舜小禺禹禽弋孑子孔贝焉燕长车歹斗方戈戉"+
            "户火斤毛木未朵来果某牛气欠犬日昔易星晓氏手水永泉瓦文心牙爻月肩朋胃能爵爪白皇癸登瓜禾秃"+
            "秋秫龙矛皿母目盾眉鸟石磬矢甲田畎番玄率穴窗用甫玉臣蜀而耳缶虎臼耒糸齐肉舍西要行羽至舟竹"+
            "自辰豆角身豕象辛酉齿阜鱼雨雷隹革鬼韭面首鬯高鬲黄鹿鼎鼠页衣羊";
        //3.形声字 特征Z
        static string word_xs = 
            "事丸书万丑丕丽举其冀匈匙冯冲决冻况冷冶净凋凉凌准凑减凛卢卦厂历厅压厌厕厘厝厩厢厨厥切"+
            "券剪劈刊刚列刘刓刎刖刨刬刭刺到刮刿剀刻刳刷剉剐剑剟剧剖剡剔剜副割剩剿剽党亏匜匡匣匽匪匿"+
            "匮队阡邦防阶阱阬那阮邪阳阿陂陈邸阽附际邻陇陀邺阻阼陌陔郊郎陋限郁郑陛除陡郛郡郤险院陨部"+
            "陲都郭陵陪陴陶陬隍隆隋随隈隅隘隔鄙鄘障隧隤隰酂兰关兹养凤却冈罔冕办劝务动助劲励努劬劭劾"+
            "势勃勉勋勍勘勒勔募勤写冢击凿余亿仇仅仍代伋仞仪仗伧伥仿份伎价伉伦仳任伤似伟优伛伯伺但低"+
            "佃伽估何伶你伾佗佚佣佑住侧侪侈佽供佳侥佼例侣侔侬侨使侍佻侠佯依侑侦俦促俄俘俭俊俚俪俩俜"+
            "俟俏俗侮修俨俑倍俾倡俶倒俸候健借俱倨倦倔倥倪俳倩倏倭倚债值偿偾假傀偻偶偏停偷偎偕偃傲傍"+
            "傧储傣傅傩傥催像儆僦僚僧僮僖僵僻儇儒千博产亩亲亵亶订讣讥认讧记讦讫让讱讪训议讹访讽讳讲"+
            "诀论讴讼讻许讶诐词诋评诎识诉译诈诊证诅诧诚诞该诟诡话诨诘诓诔诠诗试详诩询诣诤诛诶诰诲诫"+
            "诳诮说诵诱语谄调读诽课谅诺请谁谂谇诿谀诸谆诼诹谈谙谗谛谍谔谎谏谋谓谐谖谑谚谒谕谠谧谟谦"+
            "谥谢谣谨谩谬谪谰谱谭谮谴谳谶廷发变叛叙艾节芒芃芋芭苍芳芙芥芦芡芮苏苇芜芽芸芷茇苞范苟苦"+
            "茄茎苴苛苓茏茅茂茉苹苒苫苕苔英茔苑荜草茬茨荒荟荤荚荐茭荆荦茫茗茜荃荛荏茸荣茹荪荑荀药茵"+
            "荫莞荷获莱莅莲莎荼莺莹莠菜萃菲菡菅菁菊菌萝萌萋菘萎萧营萦著菑菹葆葱蒂董葛葫蒋葵落葩葺葸"+
            "萱蒉蒿蒺蓟蓝蒙蓦蓬蒲蓐蒜蓄蒸蔽蔻蔓蓰箫蔗蕉蕤蕊蔬蕴薄薨蕾薮藐藓薰藩藤藻蘖彻彷彼徂径征待"+
            "很徊律徇徒徐徜徘徙循徯徭德徽巡边辽达过迈迄迁迆迂迟返还迒近违迕迓迎远这迨迪迭迩迥迫述迢"+
            "逅迷逆适逃选逊逋逞递逗逢逦逑逡逝速通透途逍造逴逮逵逻逶逼遍遄道遁遏遑遒遂遗遐逾遇遨遘遛"+
            "遣遥遭遮遴遵避遽邂邀邅邈邃导寺寿将失头夸奅奖奎奕奢奡奥干年巧巩弘弛张弧弥弩弭弯弹强弈弊"+
            "广庇床庐庑序底店废府庙庞庖度庭庠座廊庾廋廒廓廉当录彝币布帏帐帛帘帔帖帜帙帮帧常帷帻幅帽"+
            "幂幄幕幔幛幢幡叱叮句叫叶台叹召只吃吓吐吸吁吵呈吨含吼吝呕吮听吞吻吾呀吟呵咄呷咖咍呼咀呶"+
            "咛咆呻味咏哀咤哈咳哄哗哪咷哇响哑咽咬哉咱咫咨啊唉哺唇哽哼唤唧唠哨唆唐哮唁哲唱啜啖啃售啴"+
            "唾唯啸啄喳啻喘喋喊喝喙嗟喈喟喃喷啼嗢喧喑喁喻嗞嗷嗔嗤嗝嗑嗜嗣嗅嘎嘉嘘嗾嗽嘤嘲嘬嘿噎嘶嘻"+
            "嘱噫噬嚎嚼囊驰驮驯驴驱驲骀驸驾驹驽驶驷驼驿驻驺骇骄骆骂骈骁骋骏验骖骐骑骗骚骛骘骜骞骤问"+
            "闳闷闵闱闸阀阁闺阂闿闾闽闼闻阃阅阐阊阍阎阔阑阕阖阙阗宄宇宅宏完宝宠宛宙客宣宥宾宸宽宵宴"+
            "寄寂密宿富寐寓寝察寨寮寰奶妈她妄妆妣妒妨妓妊姒妩妍妖妪姊姑姐姆妹妮始姹姜娇姣姱姥娈娜姘"+
            "娆姝娃娅姚姨姿娥姬娟娩娘娉娑娓娴娱婢婵婚婪婆婉媪媒媚嫂婷婿媸媾嫉嫁嫔嫌媵嫡嫚嫩嫱嫣嬉嬖"+
            "嬗嬮孀犷狄狂犹狗狐狎狝独狠狡狯狩狭狰狼猜猖猝猎猛猗猾猥猿岌岂屺岁屹屿岑岛岗岚岖岸岱岿岭"+
            "岫岩峦峙峡峣峥峨峰峻峭峪崩崇崔崛崎崖崭嵯嵌嵘嵎嶂巅巍彬彩影层届屈屏屐屑展屠属屦饥饬饭饪"+
            "饩饱饯饰饲饴饼饵饶饷饿馁馆馋馈馊馏馌馑馔壮声壹打扑扔扛扣扪托扬把扳扮抄扯抖扼扶抚护技拒"+
            "抉抗抠抡拟扭抛批抢扰抒抟抓拗拔抱拨拆抽担抵拂拊拐拣拉拦拢抹拇拈拧拍抨披拑抬拖押拥择拄拙"+
            "按持挡拱挂挥挤拷括挠挪拾拭拴挞挑挺挟拯挨捕挫捣换捡捐捆捞捋捏捎损捅挽捂振捉捘捭捶措掂掉"+
            "掇接捷据掘控掠描捺捻排捧掊探掏推掀掷插搀搓搭提搁搅揪揩揆揽搂揉搔握揠揖揄援掾揍摆搬搏搐"+
            "摁搢摸搦摄摊搪携摇摧撂撇摔撄摘摭播撤撑撮撩撵撬擒撒撕撷撰撞操撼擐擂擅擦擢攒攘攫攮汇汀汁"+
            "池汗江汝汤污汐沧沉泛沟汩沆沥沦沐沛沁汪沃汹沂沚泌波泊沸沽河泓浅泾沮泪泠泯沫泥泞泮泡泼泣"+
            "泗沱泄泫泱泆泳油泽沼治注测泚洞洪浒浍浑活济洎浃浇洁洛浓派洽洒洮洼洿洗涎洫洵洋洲浊涔涌涤"+
            "浮涡海浩涣浣涧浸涓浚涝涅浦润涩涘涛涕涂消浥浴涨浙淳淙淬淡淀渎涵涸淮混渐淋淖清渠深渗淑涮"+
            "淌添淅淆淹液淫淤渚渡溉港湖滑湔渴溃湄湎渺湃溲湍湾温渥湘渲湮渰渝湲渣湛滞滨滚溷溘滥溜滤满"+
            "漭溟漠滂溥溶溽溯滩溏滔滟溢源滓漕滴潢漏漫漂漆潇漩演漪潴澳潺潮澈澄澜澎潜潸澍澌潭潼濒澹激"+
            "濯瀑瀚瀛灌纠红级纪纤纫约纡纯纺纷纲纶纳纽纰纾纬纹纭纸纻纵绊绌绐绂练绍绅细线绎织终组绑绖"+
            "给绛绔络绕绒统绚绠绢绦绨绤绡绣绷绰绸绿绮绻绳绶绾维绪续综绽缁编缔缎缓缉缄缆缕缅缈缗缇缘"+
            "缊缒缤缠缞缝缚缙缛缢缜缧缦缪缥缩缨缭缮缯缴缵圣场地圹圮圩圬在坝坂坊坟坏均坎坑块圻坛址坠"+
            "坳坼垂坫坤垄坪坡坦垞垫垤垩垓垢坰垦垮垒型垠垣埃埂埒埙埠堵埵堕基培埤堑堂埸域堡塔堤堞堠堪"+
            "堰堙塍墓塑塘填境塾墉墀墩增壁壅壕壤团囤囫围园固囹囿圃圄圆圈圜夜够夥舞尔忝尝恭慕忆忏忖忙"+
            "怅忱忡怆怀忾快忸怃忤忻怖怵怛怫怪怙怜怕怦怯性怏怡怿怔恻恨恍恢恺恪恼恰恃恬恌恸恤恂恹悖悍"+
            "悔离悯悭悄悛悚悌悒悦惭惨惝惆惙悴惮悼惦惇惯惛悸惊惧惬情惕惋惘惟惜悻悲惰愕愤慌惶慨愧愣愔"+
            "愉愠惴慊慑慎慆慷慢慵懊憯憧懂憎憾懒懔懈懦懵式弑备复存孝孜孤孟孥学孩孳孵孺孽财贡贬贩货贪"+
            "贤责质贮购贲贷费贵贱贶贸贳贴贻赅贾贿赁赂赃贽赀资赉赊赈赐赌赋赓赔赏赎赒赖赛赚箦赜赝赠赡"+
            "赣毖毗毙点烈热烝烹煮煎煦照熬熊熟熹轧轨轫轩轭轮软转轲轻轶轸轴轿较辂轼载辅辆辄辍辉辎辏辐"+
            "辑输辒辖辕辘辙歼殁残殂殆殇殄殃殊殉殓殍殒殚殛殖殡殣殪斛斜斟斡卷施旁旆旌旎旐风飒飘飙爷爸"+
            "爹戏战戚戡截戮戴戳房戽所扃扈扉灭灯灿灸灵炀灼炒炊炖炕炜炬炉炮炳炯烂炼烁炭炫炷烘烬烤烙烧"+
            "烜烟烊烛烽焕焰煌煤煨煜煴煽熄熠熨燔燎燃燠爆爚观现觇觉觊觏觐觑斥斧斫新考耄耆耋毡术札机朴"+
            "权杀朽杂材杈村杜杆杠极李杞条杨杖杷板杯杻杵枫柜杭杰枥杪枪枘枢松枉枕枝杼柏枹标柄查柢栋柑"+
            "枷架柩柯枯栏柳栊枰染柔树柝枵栈栉柱案柴档格根桂核桦桓桨校框桥栓桃桐栩样桢桎株桩桌梵桴梗"+
            "梏检梨梅梢梳梭梯桶械梓棒楮棰椎棣椟棺椁楛椒棵榔棚棋棨椠棠椭椅棹植楚椽槌椿楯概槐楷楼楣楠"+
            "楔楹槁槛榴模槃榷槊榻榛槽樘横樯樱橙橱橘橹橇樵檠橐樽檀檄檐牵犁牝牲特牺犊犒版牍牌敲收攻放"+
            "玫故敌效敕救敛敏敞敦数敷氛氲次欢欤欣欷欲欿欺歃歇歈歌歉状旧旭旰旱时旸昂旻旺昕昃昳昧昵映"+
            "昭昨晖晏晕晡晦晚晤晞晛晢晻晷景晾晴暑暂暗暌暖暇暄暧暨暝暮曙曛曜簪曝曦曩祁祃祀祈祇祎祉祠"+
            "祓祜祐祗祖祚祧祥祯祷祸禅禄祺禘福禧挛挈拳掣摹搴摩擘攀殴段殿毂毁毅滕瓯瓮瓴瓷瓶甄甑甗玑玖"+
            "环玦玩玷珂玲珉珠琅理球琐琛琮琳琶琪琦琼琰瑛琢瑳瑰瑚瑙瑞瑕瑜瑑璃瑶璀璜瑾璇璎璋璞璨瓒韦韧"+
            "韨韩韪韬斋斑斐毋忌忍忒志忿忽念怂忠怠怼急怒怨怎总恶恩恚恳恐恋恁恕恙恣患悬悠惫惩惑惄惹愁"+
            "慈感愍愆想愈愿憋憨慧慰懋懿更曼曾替肌肋肠肚肝肛肓肖肤肱股胁肴胀肢肿胞背胆胡胫胧胪胚胠胜"+
            "胎胥胄胙胳胶胯脍朗脑脓胼脐朓胸脩脏脂脖脰脯脚脸脱腌腑腱腊脾期腆腕腋腴腠腭腹腻腾腰膀膊膏"+
            "膂膜膍膛膝膰膨膳臂朦臊膻臆膺臃爱攲爬的皋皑皎皓皙皤瓞瓠瓣瓤私秆种秒秘称积秣秧秩租秽秸秾"+
            "移程稍税稀稗稠稔稣稚稳稻稿穀稽稷稹稼穆穑穰钉钗钓钝钢钜钧钮钤钦钟钵钿铎铃钱钳铄铁铉钻铲"+
            "铤铗铠铐铓铭铨铜铣铦银铡铮铢锄锉锋锅铿链铺锐销铸锤错锭锢键锦锯锚锨锥锱锸镀锻镌镂锲镑镐"+
            "镒镇镖镝镜镞镣镰镶站竣童端竭龛笼矜盂盆盅盎盐盏盛盖盒盔盘簠每盯盲眈盹眊眄盼眨眩眛眠眚眷"+
            "眯眸眺眼睁眦睇睅睎督睹睢睫睛睩瞄睦睨睽瞀瞋瞌瞒瞎瞰瞥瞭瞧瞬瞳瞩瞽瞻疗疚疟疡疤疮疯疫病疽"+
            "疴疲疼痈疵痕痎痒痘痨痢痞痡痛痹痴瘁痼瘦瘟瘠瘼瘫瘳瘾瘴癞癫鸡鸠鸥鸩鸱鸳鸽鸿鸾鹅鹄鹃鹇鹏鹊"+
            "鹖鹜鹤鹬鹰皱甥矶矿砭砍砌研砚砖础砥砺砮砰破砣砧硗硕硎确硬碍碑碉碌碰碕碎碧磁磋碟磙磕碾磐"+
            "磨礁磷矣矫矮矰禁罟署罩罾电町畅畚畔畛略畦畴畸畿究穷空穹窍窈窄窕窒窖窘窦窠窟窥窭窨窾疑补"+
            "衬袄衿袂衽被袪袒裆裤裙裨裸褓褊褐褛褪褫褴褥褶襁襟襦甬玺璧臧虹虺虽蚁蚌蚕蚩蛇蛀蛤蛟蛮蛙蜒"+
            "蛰蜍蛾蜂蜉蜃蜕蝉蜚蜡螂蜜蜩蜿蜮蝶蝴蝼螯螭蟒融蟾蠢蠹耶耻耽耿耸聊聆聋职聒聘聚聪聱缸缺罄罅"+
            "罐良艰虏虑虚虞虢舅舆耙耕耗耘耜耧耦籴类粗粒粘粕粥粢粲粳粮粱粹精糇糊糅糙糕糗糖糠糜糟紊累"+
            "絮紫綦縢縻繇繄纂脔腐舐舔紬結絺綪覆衒裳裹裴褒褰翅翁翎翊翌翘翕翔翠翥翩翱翰翮翳翼翻耀肆肇"+
            "臻舱舰舶船舵舸舷舳艇艘竺笃竿笈竽笆笏笋笨笞笛第符笳笺笠笸笙笥筚策答筏筐筛筒筵筝筑筹简筠"+
            "筷签筲箔箪管箕箩箨箸箭篑篓篇箱箴篆篪篡篙篝篮篱篷簇簧篾簟簸簿籁籍释釉輣輚輮輶赧赦豁谿觚"+
            "觞触觥野量麦躬躯躲躺豢豪豫辜辟辣辨辩訢詒誉訾誓謷謇譔譬配酌酗酝酦酤酬酪酩酲酵酷酿酺酸酽"+
            "醇醋醅醍醒醢醪醴醺豹豺貂貉貌赴赳赵赶起超趁趋越趣趟趿距跂跄跃趾跋跛跌践跑跖跐跟跪跻跨跬"+
            "路跷跳跣踌踊踟踧踝踞踦踏踬踪蹉蹀蹂蹄踵蹈蹐蹇蹑蹊躇蹙蹭蹴蹲蹯蹶蹼躏躔躐龄龌靠靡釜鉴銮鏖"+
            "錡鏺鑣閤闇靖静餔餍餐饐饕鲠鲸鲲鲵鳏鳍鳖鳞雩雹零霁霆霉霈霄震霏霖霓霜霞霭霰霸露霾难雇雄雅"+
            "雁雏雎雉雌雕雠颿靶靳靴鞅鞍鞋鞘鞠鞭鞶骼骸髓魂魁魄魅魇魍魑魔靥馘馥馨顑韵韶髡髯髫髻鬓鬻駓"+
            "騃騑麋麓麒麟麾鶱鼐默黔黜黛黝黡黩黧黯黍黎黻黼鼾衢羸项颅颊颐颀衡颙顾颖颇颦顽领颁颤颠顶顿"+
            "额颔颈颗预颍颜题颂裔袭群装羲袤衙裘街袋衷裁羖";
        //4.会意字 特征U；不属于这四种的特征V
        static string word_hy =
            "临乂乎乍乔乖乘与丈无东丛丝丞两丧习乡买乱乳之义公共兴兵典具美羡匀包匏冰凝占卧厚原分争"+
            "初免划则别利删剂制剌剥劓允兄光先二元亟些区匹匠阴阵陆邮陟隙兑兼几凭卫印即卸卿内再冒功加"+
            "劣劫劳军农冠冥冤从今仄令会企众佥俞仆仁什付仟仕仙伐伏件伍休伊伫佞位作佰佩保便侵信俯倌偫"+
            "半卉华协卖县亡充享计讨讷设诂建及双友取受叟叠艺芝芬芟苗茁荡荧莽莫蒐葬蔑蔚屯往征得御微州"+
            "进连运送退逐逸对寻封尉尊天夯央夺夹夷奔奋奉奇奏套奚并左差引弢弦弱开异弄弃庆库康庸廛归彗"+
            "市师希句古号可叵史台右合后吉吏名同吹吠否告君启吴员咎命咸哥哭啮啬商喜器嚣驭驳骥闪闭闯间"+
            "闰闲闹阋阉安守宋定官审实宜宗宦室宪家害宰寇寒寡奴妃妇奸好如妥妻妾委娣婴岔岐岳彤彪彭尹尼"+
            "局尿尾履饮饺士扫报折投找拘招授掩揣汉汲沙法泅洄津流涉渔渊潀纱绞绝绥绵巽圭圳坚坐城墙壑囚"+
            "因困国囷图尧外舛多夙少尘尖尚恒愎憔幼尨就冬处夏孕孙季孰孱负贞败贯贰贼赘赢比毕羔焦爇轰辇"+
            "辔死料危旅族成戎戍戒我或戕戛戟戾扁扇灰灾灶炎炙炽烦焚燧燮既见规觅览斩断斯老耇笔毳束杲构"+
            "林枚析枭杳枣柬栗桑梁梦棘棉森牟牡牦牧牒牖改攸攽敖敢教敬款献旬早旨昌昊昏昆明春是显昼晃晋"+
            "晨晶暴氓礼社神祝承拜拿挚殷沓淼王珑班必态您悉惠意愚慝懑爽曳最有肘肥服肯育脉胤脆朔望朝爰"+
            "支此步武歪百皆甚甜秀秉科秦穗钊针钩立竞竟章竦盈盍监益盗盟監盥毒直看省相真睡睿瞢矗疾鸣鸷"+
            "皮生磊知短示祟祭票禀罗罚罢罨置罪羁甸男界畋畏畜留穿窃突窅窑窜疏虫蚀蛊耐耍聂联虐虔臾臿舀"+
            "耨粉粪紧素索累絷色艳舌行衍襄笑等筋筮算臭采辱赤赫谷解里豚辟辞詈警邑邕酋酒酣酥酱醉走足龀"+
            "金銜青食飧鲁鲜雪需霍隽雀骨髦麻黑黥鼓鼻颓颢频顷顺衰表羹衅裂";

        //级别特征词变量
        //1.级别一X
        static string wordlevel_1 =
            "一乙九了七八厂儿二几力人入十又乃丁卜刀么万三上下与习" +
            "也之义于个千及大飞干工广己已口马门山才土小子久丸丈乞乡" +
            "勺刃亏凡卫亿亡叉川寸弓巾女尸士夕中书无不专为公六历切元" +
            "五区队内办从今以化什计认反太天引开少比长车斗方风火见毛" +
            "片气日手水王文心月支分丰乏乌丹予丑勾勿匀厅允互井云匹凤" +
            "冈劝凶仓介仇仅仆仁仍升午订双友艺屯夫巨币尺扎巴忆幻尤孔" +
            "贝父户斤木牛欠犬氏瓦牙止爪业东且世主包北加务写出代们他" +
            "半去记议发节边对头平布市号叫可史只它打四外处本术民必正" +
            "白立龙目生石示电由用卡册乎乐丘丙丛丝匆占厉刊兄兰印功击" +
            "令付仙仪仔仗让讨讯训辽失央巧左归帅叨叼叮句古另叶司台叹" +
            "右召闪宁奶奴犯尼饥扒扑扔汉汇汁纠圣幼冬孕轧灭斥末未旦旧" +
            "礼永甘瓜禾矛母鸟皮甲申田穴甩玉共决压争划列则光先阶那关" +
            "再动军农会众传价件任全华产交论设许达过导并年当合各后名" +
            "同向问安好如她江红级约场地在回团因多式存成观老机权收次" +
            "有此百而米色西行至自乓乒乔丢买兴冰冲厌创刚刘刑兆亚匠防" +
            "邪阳阴阵网劣企伞仰伐仿伏伙伤似伟伪伍休优协充亦访讽讲延" +
            "芒芝巡州迈迁迅寺寻夺夹夸巩异庆庄帆师吃吊吓吉吗吐吸驰闭" +
            "闯守宇宅妇奸妈妄岂岁屿尽壮扛扣扩扫托扬执池汗汤污纪纤圾" +
            "尘尖忙孙字负贞毕轨死危爷戏灯灰考朵朴杀朽杂朱欢旬早旨曲" +
            "肌臣虫耳齐肉舌羽舟竹页衣血羊份两严况别利际即却劳但低何" +
            "你体位住作克县识证花还进近连运这张应听员间完形层局声把" +
            "报技没快我极来条改状时社求志更步每究系角里身走串丽乱兵" +
            "冻冷冶初免龟判删医阿陈附邻陆邮阻卵助劫劲励努余伯伴佛估" +
            "伶伸佣亩词评诉译诊苍芳芦芹苏芽彻役迟返违迎远寿弟弄弃床" +
            "库序希帐吧吵呈吹呆吨否告含吼君启吞呜吴呀驳驴驱闷闲宏宋" +
            "妨妙妥妖狂犹岔岛岗尿尾饭饮壳扮抄扯抖扶抚护拒抗扭抛批抢" +
            "扰折投找抓沉泛沟汽沙沈汪沃纯纺纲纳纽纱纹纸纵坝坊坏坚均" +
            "坑块坛址坐困围园怀忧孝财贡歼戒灿灵灾灶材村杜杆杠李束杏" +
            "杨牢攻旱旷忌忍忘肠肚肝皂私秃秀钉针盯疗鸡男穷补良辰赤豆" +
            "谷麦辛言足吩坟纷芬事其具到制些例使单参京该话建变取受往" +
            "府和命周定实始委拉法油治经细线织组国图性备学质转或所规" +
            "现者构果林物放明易育的直矿知空采非金青表乖丧乳典净卧厕" +
            "券兔刺刮剂刻刷降郊郎陕限郑凯凭势侧供佳佩侨侍依侦侄卖享" +
            "诚诞诗试详询叔范苦茄茎茅茂苗苹若英彼径征迫述奔奉奇幸弦" +
            "底店废庙录帘帖帜咐呼呢味咏驾驶驼驻闹闸宝官审宜宙宗姑姐" +
            "妹妻姓狗狐岸岭岩届居屈饱饰饲拔拌抱拨拆抽担抵拐拣拘拦拢" +
            "抹拍披抬拖押拥择招波泊沸河浅泪沫泥泡泼泄泻沿泳泽沾注练" +
            "绍终垂垃垄坡坦固夜尚怖怪怜怕孤季孟败贩贯货贫贪贤责购轰" +
            "轮软卷爸房炒炊炕炉炎视斧斩板杯柜杰枪松析枣枕枝牧版欧欣" +
            "昂昌昏昆旺承环玩忽念态忠肥肺肤服股肩肯朋肾胁胀肢肿武爬" +
            "秆钓盲鸣码罗畅画衬衫艰虎虏舍肃齿隶鱼雨顶顷奋美前除院养" +
            "保便信南亲说很律适选将度带品响按持指活济派给结统型复点" +
            "战标查政是段思总种科看省相研界类要重革面音须临举厚厘剑" +
            "剃削陡险卸冒勉勇冠促俘俭俊俩侵俗侮修亮亭诵误诱语叛叙草" +
            "茶荡荒茧荐茫荣药待迹迷逆送逃退追封奖奏差弯庭帮帝哀哈咳" +
            "哄哗哪咸哑咽咬咱骄骆骂阀阁闻宫客室宪宣姜娇姥娃威姨姻姿" +
            "独狠狡狮狭狱峡屋饼饺饶挡挂挥挤挎括挠挪拼拾拴挑挺挖挣测" +
            "洞洪浑浇洁津浓洽洒洗洋洲浊绑绘绞绝络绕绒巷城垫垦垮垒尝" +
            "恨恒恢恼恰孩贷费贵贺贱贸贴轻残殃施扁炮烂炼炭炸既觉览柏" +
            "柄栋架枯栏柳某染柔柿树柱牵牲故春显星映昼昨神祝祖拜泉玻" +
            "珍怠急怒怨怎胞背胆胡脉胖胜胃歪皇皆甚秒秋钞钢钩钥钟竖盆" +
            "盈毒盾眉盼眨疤疮疯疫鸦砍砌砖矩罚畏穿窃突袄虾虹蚂蚀虽蚁" +
            "耐耍缸竿赴赵趴食骨鬼首香项顺准原党部都候值调速通造验家" +
            "容展海流消圆离资热较料格根样特效能称积铁真被素般起难高" +
            "乘羞凉剥剧剖匪陵陪陶陷兼冤倍倡倘倒俯健借俱倦倾倚债读课" +
            "谅请谁谊诸谈荷获莲莫徒徐递逗逢逝透途逐射套弱座席啊唉唇" +
            "哥唤哭哨唐哲阅宾害宽宵宴宰娘娱狼狸峰屑饿壶挨捕换捡捐捆" +
            "捞捏捎损挽振捉涌浮浩浸浪涝润涉涛涂浴涨浙继绢绣埋恭悔悄" +
            "悟悦夏贿贼毙烈轿载殊旁旅爹扇烦烘烤烧烫烟烛笔案柴档桂核" +
            "桨校框栗桥桑桃桐栽株桌牺敌氧晃晋晒晌晓晕祥拿拳浆泰瓶班" +
            "珠恶恩恳恐恋息脆胳脊胶朗脑胸脏脂爱秘秤秧秩租铃铅钱钳钻" +
            "竞站监盐益盏眠病疾疲疼症鸭皱础破罢畜留窄袜袍袖蚕蚊耻耽" +
            "缺虑耕耗紧索艳翅翁致舱航舰笋笑臭辱躬酒配赶顾顽顿预颂衰" +
            "粉做得常商接据清深维基情族断教理眼着率第象领匙凑减剪副" +
            "隆随隐兽勒偿假偶偏停偷谎谜谋菠菜菊菌萝萌萍萄营著逮弹康" +
            "廊庸唱啦售唯啄骑寄寇密宿婚婆婶猜猎猫猛猪崇崖崭彩屠馆馅" +
            "掉捷掘控掠描排捧授探掏推掀掩淡混渐淋渠渗淘添淹液渔绸绩" +
            "绿绵绳绪续堵堆培堂域圈够惭惨悼惯惊惧惕惜辅辆斜旋戚毫检" +
            "梨梁梅梦梢梳梯桶械犁敢救敏欲晨晚祸球患您悉悬悠爽脖脚脸" +
            "脱望甜移铲铜银竟章笼盛盗盖盒盘眯睁痕痒鸽票略窑蛋蛇聋职" +
            "虚粗粒粘累衔船笨笛符野距跃雪雀黄鹿麻颈袭袋道强属提温就" +
            "然斯最期程确联等量越集装羡厨厦割剩隔隙傲傍储傅博谦谢谣葱董葛葵落葡葬循御逼遍遗" +
            "遇尊奥幅帽喘喊喝喉喇喷善喂喜骗阔富寒嫂猴猾屡馋插搭搁搅揭揪搂揉搜握援渡溉港湖滑" +
            "渴湿湾游渣滋编缎缓缘堡塔堤堪悲惰慌慨愧禽愉赌赔赏焦煮辈辉殖焰毯棒棍椒棵棉棚棋森" +
            "椅植棕牌敞敬散款欺晶景普晴暑暂智掌琴斑惩惠惑惹曾替朝腊脾腔登稍税稀锄锋锅链铺锐" +
            "锁销锈铸童痛鹅硬短番窗窜窝疏裤裙裕蛮蜓蛙蛛粥絮紫舒艇策答筋筐筛筒筝筑释辜超趁趋" +
            "跌践跑鲁雄雅雁黑街裁裂愤粪满照新数感想意置解路群鄙障勤催傻像谨叠蓝蒙蓬蒜蓄蒸微" +
            "遣遥廉幕嗓嫁嫌摆搬搏搞摸摄摊携摇滨滚滥溜滤漠滩滔溪源缠缝墓塞塑塌塘填慎赖煎输煌" +
            "煤楚概槐楼榆歇献暗暖福殿毁瑞愁慈愚愈腹腾腿腥腰稠锤错键锦锯锣锡盟睬督睛睡痰鹊碍" +
            "碑碌碰碎碗矮禁罩罪蛾蜂舅粮粱肆筹简签触躲辟辞誉酬酱跟跪跨跳龄鉴雹雷零雾魂韵鼓鼠" +
            "精管算酸需凳僚谱蔽蔑遭遮弊嘉嗽骡察寨嫩馒摧撇摔摘滴漏漫漂漆演缩境墙舞慕慢赛赚熊" +
            "旗截熔熄榜榴模榨敲歌歉暮璃愿膀膊膏膜稳锻锹端竭瘦碧磁疑蜡蜜蜻蝇蜘聚腐裳裹翠箩豪" +
            "辣誓酷酿貌静鲜魄鼻颗影增题劈僵僻蕉蔬德遵嘱播撤撑撒撕撞潮潜墨懂熟飘槽横橡樱暴摩" +
            "毅慧慰膛膝稻稿稼镇瞒瞎蝶蝴聪糊艘箭篇箱躺醋醉趣趟踩踏踢踪靠霉震鞋黎额颜器整凝薄" +
            "薯薪避邀嘴操激澡缴壁懒赞赠燕燃橘膨镜磨融糕糖篮辨辩醒蹄餐雕默衡颠藏骤擦赢戴燥臂" +
            "穗瞧螺糠糟繁翼辫蹈霜霞鞠镰鹰覆翻蹦鞭爆攀瓣疆警蹲颤嚼嚷灌壤耀籍躁魔蠢霸露囊罐";
        //2.级别二Y；其余Z
        static string wordlevel_2 =
            "匕刁丫巳丐歹戈曰壬夭仑亢冗讥邓卉艾夯戊凸卢叭叽叩冉皿凹囚矢乍尔卯冯玄弗弘邦迂戎" +
            "芋吏戌夷尧吁吕吆屹廷迄臼仲伦伊肋旭匈妆亥汛汝讳讶讹讼诀弛妃驮驯纫玖玛韧抠扼汞扳" +
            "抡坎抑拟抒芙芜苇芥芯芭杖杉巫甫匣吾酉尬轩卤肖吱吠呕呐吟呛吻吭邑囤吮岖牡佐佑佃伺" +
            "囱肛肘甸狈彤灸刨庇吝庐闰兑灼沐沛汰沥沦汹沧沪沁忱祀诈罕屁坠妓姊妒矣纬玫卦坷坯拓" +
            "坪坤拎拄拧拂拙拇拗茉昔苛苟苑苞茁苔枉枢枚枫杭郁矾奈殴歧卓哎咕呵咙迪呻咒咋咄咖帕" +
            "账贬贮迭氛秉岳侠侥侣侈卑刹肴觅忿肮肪狞庞疟疙疚卒庚氓炬沽沮泣泞泌沼怔怯怡宠宛祈" +
            "诡帚屉弧弥陋陌函妮姆迢叁绅驹绊绎契贰玷玲珊拭拷拱挟拽哉垢垛拯荆茸茬茵荤荧荫荔栈" +
            "柑栅柠勃柬砂泵砚鸥轴韭虐昧盹哇咧昭勋哆咪哟贻幽钙钝钠钦钧钮毡氢俏俄俐禹侯徊衍胚" +
            "胧胎狰饵峦咨闺闽籽娄兹烁炫洼柒洛浏恃恍恬恤宦诫诬祠诲屏屎逊陨姚娜癸蚤绚骇耘耙秦" +
            "匿埂捂捍袁捌挫挚捣捅埃耿聂莽莱莉莹莺梆桔栖桦栓桩贾酌砸砰砾殉逞哮唠哺剔蚌畔蚣蚪" +
            "蚓哩圃哦鸯唁哼唧唆峭峨峻赂赃钾氨俺赁倔殷耸舀豺豹颁胰脐脓逛卿鸵鸳馁凌凄衷郭斋疹" +
            "紊瓷羔烙浦涡涣涤涧涕涩悖悍悯窍诺诽冥谆祟娟恕娥骏琐琉琅措捺焉捶赦埠捻掐掂掷掺聆" +
            "勘聊娶菱菲萎菩萤乾萧萨菇彬婪梗梧梭曹酝酗厢硅硕奢盔匾颅彪眶啪曼晦啡趾啃蚯蛀唬鄂" +
            "啰唾啤啥啸崎逻崔帷崩崛婴铐铛铝铭矫秸秽笙偎躯兜衅徘徙舶舵敛脯豚逸凰猖祭烹庶庵痊" +
            "阎阐羚眷焊焕鸿涯淑淌淮淆渊淫淳淤淀涮涵惟惦悴惋寅寂窒谍谐袱祷谓谚尉隋堕隅婉颇绰" +
            "绷综绽缀巢琳琢琼揍堰揩揽彭揣搀搓壹搔葫募蒋蒂韩棱椰焚椎棺榔椭粟棘酣酥硝硫颊雳翘" +
            "凿棠睐晰鼎喳遏晾畴跋跛蜒蛤鹃喻啼喧嵌赋赎赐锌甥掰氮氯筏皓皖粤逾腋腕猩猬惫馈敦斌" +
            "痘痢痪竣翔奠遂滞湘渤渺溃溅湃渝渲愕愣惶寓窖窘雇禅禄谤犀隘媒媚婿缅缆缉缔缕骚瑟鹉" +
            "瑰瑙聘斟靴靶蒲蓉椿楷榄酪碘尴辐辑频睹睦瞄睫嗜嗦暇畸跷跺跤蜈蜗蜕嗅嗡署蜀幌锚锥锰" +
            "稚颓筷魁衙腻腮腺鹏鲍猿颖煞雏馍馏禀痹廓痴靖誊滇漓溢溯溶溺寞窥窟寝褂裸谬媳嫉缚缤" +
            "剿赘熬墟赫摹蔓蔡蔗蔼熙蔚兢槛榕酵碟碱碳辖辗雌瞅墅踊蝉嘛嘀镀舔熏箕箫舆僧魅孵瘩瘟" +
            "彰粹煽潇漱漾慷寡寥谭肇褐褪隧撵撩撮撬擒墩撰鞍蕊蕴樟橄敷豌醇磕磊磅碾霄嘻嘶嘲嘹蝠" +
            "蝎蝌蝗蝙嘿幢镐镑稽篓鲤鲫褒瘪瘤瘫凛憋澎潭鲨澳潘澈澜澄憔懊憎翩褥谴鹤憨履豫缭撼擂" +
            "擅蕾薛薇擎翰噩橱橙瓢霍霎辙冀踱蹂蟆螃噪鹦黔穆篡篷篱儒鲸瘾瘸糙濒憾懈窿缰藉藐檬檐" +
            "檀礁磷瞭瞬瞳瞩瞪曙蹋蟋蟀嚎赡魏簧簇徽爵朦臊鳄癌懦豁臀藕藤瞻嚣鳍瀑襟璧戳孽蘑藻曝" +
            "蹭蹬巅簸簿蟹靡癣羹鳖鬓馨蠕巍鳞糯譬霹躏黯髓赣镶瓤矗";

        //fs特征词（需要从文本文档中读入）
        static ArrayList fs_f = getFeaturWords(@"../../Resources/FSWords/f.txt");//f特征词F
        static ArrayList fs_h = getFeaturWords(@"../../Resources/FSWords/h.txt");//h特征词H
        static ArrayList fs_s = getFeaturWords(@"../../Resources/FSWords/s.txt");//s特征词S
        static ArrayList fs_t = getFeaturWords(@"../../Resources/FSWords/t.txt");//t特征词T
        static ArrayList fs_w = getFeaturWords(@"../../Resources/FSWords/w.txt");//w特征词W；其他0
        

        //对输入的字符串调用NLPIR分词处理并写入NLPIR.txt
        public static string ParagraphProcess(string paragraph)
        {
            if (!NLPIRTool.NLPIR_Init(@"../../Resources/ICTCLAS", 0, ""))//给出Data文件所在的路径，注意根据实际情况修改。
            {
                throw new Exception("Init ICTCLAS failed!");
            }
            NLPIRTool.NLPIR_SetPOSmap(3);//使用北大一级标注
            paragraph = paragraph.Replace("\n"," ");//将换行符替换为空
            IntPtr intPtr = NLPIRTool.NLPIR_ParagraphProcess(paragraph.Trim());//切分结果保存为IntPtr类型
            String str = Marshal.PtrToStringAnsi(intPtr);//将切分结果转换为string，将空格替换为制表符'\n'
            str = str.Replace(' ', '\n').Replace("\n\n","\n");
            StreamWriter sw = new StreamWriter(NLPIRAdress,false,Encoding.Default);
            sw.WriteLine(str);
            sw.Close();
            NLPIRTool.NLPIR_Exit();
            return str;
        }

        /* 方法：词性和构词处理
         * 参数：字符串；形如：“文选/n”，经过NLPIR分词后的单词
         * 返回值：字符串； 形如：文   N   T
         *                        选   N   W，间隔符为'\t'
         * 构词原理：
         * 标点B；英文/数字E；单个字D；词组T开头Z中间W结尾
         */
        public static string NLPIRWordProcess(string input_word)
        {
            //判断字符串是否符合格式“词/词性”，只为了判断空行
            if (!input_word.Contains('/')) return "";

            //创建正则表达式，用来判断字符串中内容
            Regex cn = new Regex(@"^[\u4e00-\u9fa5]*$");//由中文字符组成
            Regex en_num = new Regex(@"^[A-Za-z0-9]*$");//数字和英文

            string result = "初始化字符\n";

            //避免使用split()方法，因为词语可能本身就自带'/'符号
            string cx = input_word[input_word.LastIndexOf('/')+1].ToString().ToUpper();//最后一个'/'字符的后一位为词性
            string word = input_word.Remove(input_word.LastIndexOf('/'));//取去掉最一个'/'后的所有字符的新串为单词

            //进行逻辑判断并进行词性和构词处理
            if (cx[0] == 'W' || Punctuations.Contains(cx[0]))
            {//1.如果为标点（标注为/w）或存在于定义的标点字符串中，则取第一个字符加入result,前为词性标注，后为构词标注
                result = word + "\t" + cx + "\tB";
            }
            else
            {//2.如果不为标点
                if (word.Length == 1)
                {//2.1 如果单词长度为1
                    if (cn.IsMatch(word))
                    {//2.1.1 如果为一个汉字
                        result = word + "\t" + cx + "\tD";
                    }
                    else
                    {//2.1.2 如果是非汉字
                        result = word + "\t" + cx + "\tE";
                    }
                }
                else
                {//2.2 如果单词长度>=2
                    if (!Regex.IsMatch(word, @"[\u4e00-\u9fa5]"))
                    {//2.3.1如果不含中文,需考虑特殊字符的用法：'-'和出现在末尾的':'
                        if (word.Contains('-'))
                        {//2.3.1.1 如果包含-符号而分词未分开
                            string[] words = word.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                            result = "";
                            if (words.Length == 1)
                            {//如果拆分后只有一个，即'-'出现在首尾
                                if (word[0] == '-')
                                {
                                    result += '-' + "\t" + cx + "\tB\n";
                                    result += words[0] + "\t" + cx + "\tE";
                                }
                                else
                                {
                                    result += words[0] + "\t" + cx + "\tE\n";
                                    result += '-' + "\t" + cx + "\tB";
                                }
                                
                            }
                            else
                            {//拆分后单词大于等于2，即'-'出现在词中
                                //将除最后一项加入result字符串
                                for (int i = 0; i < words.Length - 1; i++)
                                {
                                    result += words[i] + "\t" + cx + "\tE\n";
                                    result += '-' + "\t" + cx + "\tB\n";
                                }
                                //将最后一项加入result字符串
                                result += words[words.Length - 1] + "\t" + cx + "\tE";
                            }
                        }
                        else if (word[word.Length - 1] == ':')
                        {//2.3.1.2 最后一个字符为':'而分词未分开
                            result = word.Remove(word.Length - 1, 1) + "\t" + cx + "\tE\n";
                            result += ':' + "\t" + cx + "\tB";
                        }
                        else
                        {//2.3.1.3 如果不含标点，是其他字符串
                            result = word + "\t" + cx + "\tE";
                        }
                    }
                    else if (Regex.Matches(word, @"[\u4e00-\u9fa5]").Count == 1)
                    {//2.3.2 如果字符串中含有一个中文字符（多为量词+中文单位）
                        int index = Regex.Match(word, @"[\u4e00-\u9fa5]").Index;
                        if (index == 0)
                        {//中文出现在第一位
                            result = word[0] + "\t" + cx + "\tD\n";
                            result += word.Remove(0, 1) + "\t" + cx + "\tE";
                        }
                        else if (index == word.Length - 1)
                        {//中文出现在末尾
                            char cstr = word[word.Length - 1];
                            result = word.Remove(word.Length - 1, 1) + "\t" + cx + "\tE\n";
                            result += cstr + "\t" + cx + "\tD";
                        }
                        else
                        {//中文出现在词中部
                            string[] words = word.Split(word[index]);
                            for (int i = 0; i < words.Length - 1; i++)
                            {
                                result = words[i] + "\t" + cx + "\tE\n";
                                result += word[index] + "\t" + cx + "\tD\n";
                            }
                            //将最后一项加入result字符串
                            result += words[words.Length - 1] + "\t" + cx + "\tE";
                        }
                    }
                    else
                    {//2.3.3 如果字符串中含有两个及以上中文字符,TZW
                        result = word[0] + "\t" + cx + "\tT\n";
                        for (int i = 1; i < word.Length - 1; i++)
                        {
                            result += word[i] + "\t" + cx + "\tZ\n";
                        }
                        result += word[word.Length - 1] + "\t" + cx + "\tW";
                    }
                }

            }
            return result;
        }
        /*方法：分类处理
         *输入：string字符串；形如“文   N   T”；
         *输出：string字符串；形如“文   N   T   X”；
         *原理：判断输入字符串第一个字符是否为分类字符串中的几类，添加分类特征
         *      特征值：指事字X象形字Y形声字Z会意字U其他V
         */
        public static string ClassifyWordProcess(string input_word)
        {
            //不考虑输入字符不符合格式,只考虑有空行
            if (input_word.Length < 2) return input_word;
            string result = input_word;
            string word = input_word.Split('\t')[0];
            if(word_zs.Contains(word))
            {
                result += "\tX";
            }
            else if(word_xx.Contains(word))
            {
                result += "\tY";
            }
            else if(word_xs.Contains(word))
            {
                result += "\tZ";
            }
            else if(word_hy.Contains(word))
            {
                result += "\tU";
            }
            else
            {
                result += "\tV";
            }
            return result;
        }
        /*方法：级别处理
         *输入：string字符串；形如“文   N   T   X”；
         *输出：string字符串；形如“文   X   N   T   X”；
         *原理：判断输入字符串第一个字符是否为级别字符串中的几类，添加级别特征
         *      特征值：级别一X级别二Y其他Z
         */
        public static string LevelWordProcess(string input_word)
        {
            if (input_word.Length < 2) { return input_word; }
            string[] words = input_word.Split('\t');
            string result = "";
            string word = input_word.Split('\t')[0];
            //放在words[0]后一个
            if (wordlevel_1.Contains(word))
            {
                result = words[0] + "\tX\t" + words[1] + "\t" + words[2] + "\t" + words[3];
            }
            else if (wordlevel_2.Contains(word))
            {
                result = words[0] + "\tY\t" + words[1] + "\t" + words[2] + "\t" + words[3];
            }
            else { result = words[0] + "\tZ\t" + words[1] + "\t" + words[2] + "\t" + words[3]; }
            return result;
        }

        public static string FSWordProcess(string input_word)
        {
            if (input_word.Length < 2) { return input_word; }
            string[] words = input_word.Split('\t');
            string result = "";
            string word = input_word.Split('\t')[0];
            //放在words[2]和words[3]之间
            if(fs_f.Contains(word))
            {
                result = words[0] + "\t" + words[1] + "\t" + words[2] + "\tF\t" + words[3] + "\t" + words[4];
            }
            else if (fs_h.Contains(word))
            {
                result = words[0] + "\t" + words[1]+ "\t" + words[2] + "\tH\t" +  words[3] + "\t" + words[4];
            }
            else if (fs_s.Contains(word))
            {
                result = words[0] + "\t" + words[1] + "\t" + words[2] + "\tS\t" + words[3] + "\t" + words[4];
            }
            else if (fs_t.Contains(word))
            {
                result = words[0] + "\t" + words[1] + "\t" + words[2] + "\tT\t" + words[3] + "\t" + words[4];
            }
            else if (fs_w.Contains(word))
            {
                result = words[0] + "\t" + words[1] + "\t" + words[2] + "\tW\t" + words[3] + "\t" + words[4];
            }
            else
            {
                result = words[0] + "\t" + words[1] + "\t" + words[2] + "\t0\t" + words[3] + "\t" + words[4];
            }
            return result;
        }

        //按以下四个过程顺序添加五个特征
        /*过程1
         * 对NLPIR.txt文件进行词性和构词处理，保存结果到Classify.txt文档中
         */
        public static void NLPIRProcess()
        {
            StreamWriter sw = new StreamWriter(ClassifyAdress,false,Encoding.Default);
            StreamReader sr = new StreamReader(NLPIRAdress, Encoding.Default);

            string strLine = sr.ReadLine();          
            while (strLine != null)
            {
                sw.WriteLine(NLPIRWordProcess(strLine));
                strLine = sr.ReadLine();
            }  
            sw.Close();
            sr.Close();
        }
        //过程2-4可以合并，一步到最后，这里为了保存中间结果文件分开进行
        /*过程2
         * 对Classify.txt文件进行分类处理，保存结果到Level.txt文档中
         */
        public static void ClassifyProcess()
        {
            StreamWriter sw = new StreamWriter(LevelAdress, false, Encoding.Default);
            StreamReader sr = new StreamReader(ClassifyAdress, Encoding.Default);

            string strLine = sr.ReadLine();          
            while (strLine != null)
            {
                sw.WriteLine(ClassifyWordProcess(strLine));
                strLine = sr.ReadLine();
            }  
            sw.Close();
            sr.Close();
        }
        /*过程3
         * 对Level.txt文件进行级别处理，保存结果到FS.txt文档中
         */
        public static void LevelProcess()
        {
            StreamWriter sw = new StreamWriter(FSAdress, false, Encoding.Default);
            StreamReader sr = new StreamReader(LevelAdress,Encoding.Default);

            string strLine = sr.ReadLine();
            while (strLine != null)
            {
                sw.WriteLine(LevelWordProcess(strLine));
                strLine = sr.ReadLine();
            }
            sw.Close();
            sr.Close();
        }
        /*过程4
         * 对FS.txt文件进行fs特征处理，保存结果到Features.txt文档中，得到五个特征的文档
         */
        public static void FSProcess()
        {
            StreamWriter sw = new StreamWriter(FeaturesAdress, false, Encoding.Default);
            StreamReader sr = new StreamReader(FSAdress, Encoding.Default);

            string strLine = sr.ReadLine();
            while (strLine != null)
            {
                sw.WriteLine(FSWordProcess(strLine));//去掉每一行的换行符
                strLine = sr.ReadLine();
            }
            sw.Close();
            sr.Close();
        }
        /*过程5
         * 对Features.txt文件进行cmd调用CRFS的crfs_test函数进行测试，结果保存到Results.txt文件中
         * 参数：导入的model名称
         * 对于控制台输出信息可以返回
         */
        public static void CRFSProcess(string model)
        {
            //运行路径
            string path = System.Environment.CurrentDirectory + @"\..\..";
            //调用命令，选择导入的model
            string command = @".\Resources\CRFS\crf_test -m .\Resources\Models\"+ model +@" .\ProcessingFiles\Features.txt > .\ProcessingFiles\Results.txt";

            Process p = new Process();// 打开一个新进程  
            p.StartInfo.FileName = "cmd.exe";// 指定进程程序名称 
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(path[0] + ":");//到指定盘符
            p.StandardInput.WriteLine("cd " + path);//到制定文件路径
            p.StandardInput.WriteLine(command + " &exit");//执行调用命令
            p.StandardInput.AutoFlush = true;

            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
        }
        /*过程6
         * 读取Results.txt文件，对其最后一列属性值判断，找出关键词并返回关键词
         */
        public static ArrayList GetKeyWords()
        {
            StreamReader sr = new StreamReader(ResultAdress, Encoding.Default);
            ArrayList result = new ArrayList();
            string temp = "";

            string strLine = sr.ReadLine();
            while (strLine != null)
            {
                if (strLine == "")
                {
                    strLine = sr.ReadLine(); 
                    continue;
                }
                if (strLine.Last() == 'B')
                {
                    temp += strLine.Split('\t')[0];
                    strLine = sr.ReadLine();
                    while (strLine != null && strLine.Last() == 'M')
                    {
                        temp += strLine.Split('\t')[0];
                        strLine = sr.ReadLine();
                    }
                    temp += strLine.Split('\t')[0];
                    if (!result.Contains(temp)) result.Add(temp);//防止重复
                    temp = "";
                }
                strLine = sr.ReadLine();
            }
            sr.Close();
            return result;
        }


        //方法：按行读取文件，每行保存到一个ArrayList对象中，并返回该对象。
        //参数：文件路径
        //返回值：ArrayList
        public static ArrayList getFeaturWords(string path)
        {
            ArrayList result = new ArrayList();
            StreamReader sr = new StreamReader(path,Encoding.Default);
            string str = sr.ReadLine();
            while (str != null)
            {
                str = str.TrimStart('"').TrimEnd('"');//由文件格式，去掉每行前后的'"'号
                result.Add(str);
                str = sr.ReadLine();
            }
            sr.Close();
            return result;
        }

        //数据库连接测试
        public static string MainProcess()
        {
            string title_str = "";

            DataSet ds =  DataAccess.GetTitleById("'11A0012014010001'");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                title_str += ds.Tables[0].Rows[i]["篇名"].ToString().Trim() + "\n";
            }
            return title_str;
        }

    }
}
