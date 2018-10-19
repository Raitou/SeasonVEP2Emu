using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice.GameInf
{
    public struct Package
    {
        public int PackageID;
        public uint helm;
        public uint upper;
        public uint lower;
        public uint gloves;
        public uint shoes;
        public uint weapon;
    }

    public static class Package_sInfo
    {
        public static Package[] PackageList = new Package[744];

        public static void Create_Package(int position, int packageid, uint helm, uint weapon)
        {
            uint CurrentItem = 0;
            PackageList[position].PackageID = packageid;
            PackageList[position].helm = helm;
            CurrentItem = helm;

            PackageList[position].upper = CurrentItem + 10;

            PackageList[position].lower = CurrentItem + 20;
            PackageList[position].gloves = CurrentItem + 30;
            PackageList[position].shoes = CurrentItem + 40;
            PackageList[position].weapon = weapon;
        }

        public static void LoadPackage_sInfo()
        {
            //Adventurer's
            Create_Package(0, 379810, 379820, 414410);
            Create_Package(1, 379870, 379880, 414420);
            Create_Package(2, 379930, 379940, 414430);
            Create_Package(3, 379990, 380000, 414440);
            Create_Package(4, 38050, 380060, 414450);
            Create_Package(5, 38110, 380120, 414460);
            Create_Package(6, 38170, 380180, 414470);
            Create_Package(7, 38230, 380240, 414480);
            Create_Package(8, 380290, 380300, 414490);
            Create_Package(9, 380350, 380360, 414500);
            Create_Package(10, 448100, 448050, 448110);
            Create_Package(11, 518830, 518840, 522690);
            Create_Package(12, 690520, 690530, 697060);
            Create_Package(13, 704910, 704920, 710900);
            Create_Package(14, 859590, 859600, 865530);
            Create_Package(15, 930770, 930780, 938040);
            Create_Package(16, 1158390, 1158400, 1171340);
            Create_Package(17, 1248490, 1248500, 1261370);

            //Roman Armor Set
            Create_Package(18, 397200, 397210, 421740);
            Create_Package(19, 397260, 397270, 421750);
            Create_Package(20, 397320, 397330, 421760);
            Create_Package(21, 397380, 397390, 421770);
            Create_Package(22, 397440, 397450, 421780);
            Create_Package(23, 397500, 397510, 421790);
            Create_Package(24, 397560, 397570, 421800);
            Create_Package(25, 397620, 397630, 421810);
            Create_Package(26, 397680, 397690, 421820);
            Create_Package(27, 397740, 397750, 421830);
            Create_Package(28, 454430, 454440, 452690);
            Create_Package(29, 520460, 520470, 522700);
            Create_Package(30, 692460, 692470, 696520);
            Create_Package(31, 706850, 706860, 710910);
            Create_Package(32, 861530, 861540, 865540);
            Create_Package(33, 933820, 933830, 938380);
            Create_Package(34, 1162600, 1162610, 1171750);
            Create_Package(35, 1252700, 1252710, 1261780);

            //Elite Roman Armor Set           
            Create_Package(36, 397200, 397210, 421740);
            Create_Package(37, 397260, 397270, 421750);
            Create_Package(38, 397320, 397330, 421760);
            Create_Package(39, 397380, 397390, 421770);
            Create_Package(40, 397440, 397450, 421780);
            Create_Package(41, 397500, 397510, 421790);
            Create_Package(42, 397560, 397570, 421800);
            Create_Package(43, 397620, 397630, 421810);
            Create_Package(44, 397680, 397690, 421820);
            Create_Package(45, 397740, 397750, 421830);
            Create_Package(46, 454430, 454440, 452690);
            Create_Package(47, 520460, 520470, 522700);
            Create_Package(48, 692460, 692470, 696520);
            Create_Package(49, 706850, 706860, 710910);
            Create_Package(50, 861530, 861540, 865540);
            Create_Package(51, 933820, 933830, 938380);
            Create_Package(52, 1162600, 1162610, 1171750);
            Create_Package(53, 1252700, 1252710, 1261780);

            //Dolmen Armor Set
            Create_Package(54, 381010, 381020, 414610);
            Create_Package(55, 381070, 381080, 414620);
            Create_Package(56, 381130, 381140, 414630);
            Create_Package(57, 381190, 381200, 414640);
            Create_Package(58, 381250, 381260, 414650);
            Create_Package(59, 381310, 381320, 414660);
            Create_Package(60, 381370, 381380, 414670);
            Create_Package(61, 381430, 381440, 414680);
            Create_Package(62, 381490, 381500, 414690);
            Create_Package(63, 381550, 381560, 414700);
            Create_Package(64, 452700, 452710, 452760);
            Create_Package(65, 518950, 518960, 522710);
            Create_Package(66, 690700, 690710, 696530);
            Create_Package(67, 705090, 705100, 710920);
            Create_Package(68, 859770, 859780, 865550);
            Create_Package(69, 930950, 930960, 938060);
            Create_Package(70, 1158570, 1158580, 1171360);
            Create_Package(71, 1248670, 1248680, 1261390);

            //Viking Armor Set
            Create_Package(72, 397800, 397810, 421840);
            Create_Package(73, 397860, 397870, 421850);
            Create_Package(74, 397920, 397930, 421860);
            Create_Package(75, 397980, 397990, 421870);
            Create_Package(76, 398040, 398050, 421880);
            Create_Package(77, 398100, 398110, 421890);
            Create_Package(78, 398160, 398170, 421900);
            Create_Package(79, 398220, 398230, 421910);
            Create_Package(80, 398280, 398290, 421920);
            Create_Package(81, 398340, 398350, 421930);
            Create_Package(82, 454500, 454510, 454560);
            Create_Package(83, 520520, 520530, 522940);
            Create_Package(84, 692520, 692530, 696760);
            Create_Package(85, 706910, 706920, 711150);
            Create_Package(86, 861590, 861600, 865770);
            Create_Package(87, 933880, 933890, 938390);
            Create_Package(88, 1162660, 1162670, 1171760);
            Create_Package(89, 1252760, 1252770, 1261790);


            //Fire Strike Armor Set
            Create_Package(90, 398400, 398410, 421940);
            Create_Package(91, 398460, 398470, 421950);
            Create_Package(92, 398520, 398530, 421960);
            Create_Package(93, 398580, 398590, 421970);
            Create_Package(94, 398640, 398650, 421980);
            Create_Package(95, 398700, 398710, 421990);
            Create_Package(96, 398760, 398770, 422000);
            Create_Package(97, 398820, 398830, 422010);
            Create_Package(98, 398880, 398890, 422020);
            Create_Package(99, 398940, 398950, 422030);
            Create_Package(100, 454570, 454580, 454630);
            Create_Package(101, 520580, 520590, 522950);
            Create_Package(102, 692580, 692590, 696770);
            Create_Package(103, 706970, 706980, 711160);
            Create_Package(104, 861650, 861660, 865780);
            Create_Package(105, 933940, 933950, 938400);
            Create_Package(106, 1162720, 1162730, 1171770);
            Create_Package(107, 1252820, 1252830, 1261800);


            //Traveler's Armor Set
            Create_Package(108, 381610, 381620, 414710);
            Create_Package(109, 381670, 381680, 414720);
            Create_Package(110, 381730, 381740, 414730);
            Create_Package(111, 381790, 381800, 414740);
            Create_Package(112, 381850, 381860, 414750);
            Create_Package(113, 381910, 381920, 414760);
            Create_Package(114, 381970, 381980, 414770);
            Create_Package(115, 382030, 382040, 414780);
            Create_Package(116, 382090, 382100, 000000);
            Create_Package(117, 382150, 382160, 414800);
            Create_Package(118, 452770, 452780, 452830);
            Create_Package(119, 519010, 519020, 522720);
            Create_Package(120, 690820, 690830, 696540);
            Create_Package(121, 705210, 705220, 710930);
            Create_Package(122, 859890, 859900, 865560);
            Create_Package(123, 931070, 931080, 938070);
            Create_Package(124, 1158690, 1158700, 1171370);
            Create_Package(125, 1248790, 1248800, 1261400);

            //Trooper's Armor Set
            Create_Package(126, 382210, 382220, 414810);
            Create_Package(127, 382270, 382280, 414830);
            Create_Package(128, 382330, 382340, 414850);
            Create_Package(129, 382390, 382400, 414870);
            Create_Package(130, 382450, 382460, 414890);
            Create_Package(131, 382510, 382520, 414910);
            Create_Package(132, 382570, 382580, 414930);
            Create_Package(133, 382630, 382640, 414950);
            Create_Package(134, 382690, 382700, 414970);
            Create_Package(135, 382750, 3382760, 414990);
            Create_Package(136, 452840, 452850, 452900);
            Create_Package(137, 519070, 519080, 522730);
            Create_Package(138, 690940, 690950, 696550);
            Create_Package(140, 705330, 705340, 710940);
            Create_Package(141, 860010, 860020, 865570);
            Create_Package(142, 931190, 931200, 938080);
            Create_Package(143, 1158810, 1158820, 1171380);
            Create_Package(144, 1248910, 1248920, 1261410);

            //Nazka Armor Set
            Create_Package(145, 399000, 399010, 422040);
            Create_Package(146, 399060, 399070, 422060);
            Create_Package(147, 399120, 399130, 422080);
            Create_Package(148, 399180, 399190, 422100);
            Create_Package(149, 399240, 399250, 422120);
            Create_Package(150, 399300, 399310, 422140);
            Create_Package(151, 399360, 399370, 422160);
            Create_Package(152, 399420, 399430, 422180);
            Create_Package(153, 399480, 399490, 422200);
            Create_Package(154, 399540, 399550, 422220);
            Create_Package(155, 454640, 454650, 454700);
            Create_Package(156, 520640, 520650, 522960);
            Create_Package(157, 692640, 692650, 696780);
            Create_Package(158, 707030, 707040, 711170);
            Create_Package(159, 861710, 861720, 865790);
            Create_Package(160, 934000, 934010, 938410);
            Create_Package(161, 1162780, 1162790, 1171780);
            Create_Package(162, 1252880, 1252890, 1261810);

            //Crystalline Armor Set
            Create_Package(163, 399600, 399610, 422240);
            Create_Package(164, 399670, 399680, 422260);
            Create_Package(165, 399740, 399750, 422280);
            Create_Package(166, 399810, 399820, 422300);
            Create_Package(167, 399880, 399890, 422320);
            Create_Package(168, 399950, 399960, 422340);
            Create_Package(169, 400020, 400030, 422360);
            Create_Package(170, 400090, 400100, 422380);
            Create_Package(171, 400160, 400170, 422400);
            Create_Package(172, 400230, 399550, 422420);
            Create_Package(173, 454710, 454720, 454780);
            Create_Package(174, 520700, 520710, 522970);
            Create_Package(175, 692700, 692710, 696790);
            Create_Package(178, 707090, 707100, 711180);
            Create_Package(179, 861770, 861780, 865800);
            Create_Package(180, 934070, 934080, 938420);
            Create_Package(181, 1162850, 1162860, 1171790);
            Create_Package(182, 1252950, 1252960, 1261820);

            //Righteous Guardian Armor Set
            Create_Package(183, 382810, 382820, 415010);
            Create_Package(184, 382880, 382890, 415030);
            Create_Package(185, 382950, 382960, 415050);
            Create_Package(186, 383020, 383030, 415070);
            Create_Package(187, 383090, 383100, 415090);
            Create_Package(188, 383160, 383170, 415110);
            Create_Package(189, 383230, 383240, 415130);
            Create_Package(190, 383300, 383310, 415150);
            Create_Package(191, 383370, 383380, 415170);
            Create_Package(192, 383440, 383450, 415190);
            Create_Package(193, 452910, 452920, 452980);
            Create_Package(194, 519130, 519140, 522740);
            Create_Package(195, 691060, 691070, 696560);
            Create_Package(196, 705450, 705460, 710950);
            Create_Package(197, 860130, 860140, 865580);
            Create_Package(198, 931310, 931320, 938090);
            Create_Package(199, 1158930, 1158940, 1171390);
            Create_Package(200, 1249030, 1249040, 1261420);

            //Monk Armor Set
            Create_Package(201, 383510, 383520, 436600);
            Create_Package(202, 383580, 383590, 436620);
            Create_Package(203, 383650, 383660, 436640);
            Create_Package(204, 383720, 383730, 436660);
            Create_Package(205, 383790, 383800, 436680);
            Create_Package(206, 383860, 383870, 436700);
            Create_Package(207, 383930, 383940, 436720);
            Create_Package(208, 384000, 384010, 436740);
            Create_Package(209, 384070, 384080, 436760);
            Create_Package(230, 384140, 384150, 436780);
            Create_Package(231, 384210, 384220, 000000);
            Create_Package(232, 452990, 453000, 456220);
            Create_Package(233, 519200, 519210, 523160);
            Create_Package(234, 691200, 691210, 696980);
            Create_Package(235, 705590, 705600, 710970);
            Create_Package(236, 860410, 860420, 711370);
            Create_Package(237, 860270, 860280, 865990);
            Create_Package(238, 931450, 931460, 938280);
            Create_Package(239, 1159070, 1159080, 1171650);


            //Absolute Power Armor Set
            Create_Package(240, 400300, 400310, 422440);
            Create_Package(241, 400370, 400380, 422460);
            Create_Package(242, 400440, 400450, 422480);
            Create_Package(243, 400510, 400520, 422500);
            Create_Package(244, 400580, 400590, 422520);
            Create_Package(245, 400650, 400660, 422540);
            Create_Package(246, 400720, 400730, 422560);
            Create_Package(247, 400790, 400800, 422580);
            Create_Package(248, 400860, 400870, 422600);
            Create_Package(249, 400930, 400940, 422620);
            Create_Package(250, 454790, 454800, 454860);
            Create_Package(251, 520770, 520780, 522980);
            Create_Package(252, 692770, 692780, 696800);
            Create_Package(253, 707160, 707170, 711190);
            Create_Package(254, 861840, 861850, 865810);
            Create_Package(255, 934140, 934150, 938430);
            Create_Package(256, 1162920, 1162930, 1171800);
            Create_Package(257, 1253020, 1253030, 1261830);

            //Highlander Armor Set
            Create_Package(258, 401070, 401080, 422650);
            Create_Package(259, 401140, 401150, 422680);
            Create_Package(260, 401210, 401220, 422710);
            Create_Package(261, 401280, 401290, 422740);
            Create_Package(262, 401350, 401360, 422770);
            Create_Package(263, 401420, 401430, 422800);
            Create_Package(264, 401490, 401500, 422830);
            Create_Package(265, 401560, 401570, 422860);
            Create_Package(266, 401630, 401640, 422890);
            Create_Package(267, 401700, 401710, 422920);
            Create_Package(268, 454870, 454880, 454940);
            Create_Package(269, 520840, 520850, 522990);
            Create_Package(270, 692840, 692850, 696810);
            Create_Package(271, 707230, 707240, 711200);
            Create_Package(272, 861910, 861920, 865820);
            Create_Package(273, 934210, 934220, 938440);
            Create_Package(274, 1162990, 1163000, 1171810);
            Create_Package(275, 1253090, 1253100, 1261840);

            //Raider Armor Set
            Create_Package(276, 384280, 384290, 415420);
            Create_Package(277, 384350, 384360, 415450);
            Create_Package(278, 384420, 384430, 415480);
            Create_Package(279, 384490, 384500, 415510);
            Create_Package(280, 384560, 384570, 415540);
            Create_Package(281, 384630, 384640, 415570);
            Create_Package(282, 384700, 384710, 415600);
            Create_Package(283, 384770, 384780, 415630);
            Create_Package(284, 384840, 384850, 415660);
            Create_Package(285, 384910, 384920, 415690);
            Create_Package(286, 384980, 384990, 415720);
            Create_Package(287, 453070, 453080, 453140);
            Create_Package(288, 519270, 519280, 522760);
            Create_Package(289, 691340, 691350, 696580);
            Create_Package(290, 705730, 705740, 710970);
            Create_Package(291, 860410, 860420, 865600);
            Create_Package(292, 931590, 931600, 938110);
            Create_Package(293, 1159210, 1159220, 1171410);
            Create_Package(294, 1249310, 1249320, 1261440);

            //Moonlight Armor Set
            Create_Package(295, 385050, 385060, 415730);
            Create_Package(296, 385120, 385130, 415760);
            Create_Package(297, 385190, 385200, 415790);
            Create_Package(298, 385260, 385270, 415820);
            Create_Package(299, 385330, 385340, 415850);
            Create_Package(230, 385400, 385410, 415880);
            Create_Package(231, 385470, 385480, 415910);
            Create_Package(232, 385540, 385550, 415940);
            Create_Package(233, 385610, 385620, 415970);
            Create_Package(234, 385680, 385690, 416000);
            Create_Package(235, 385750, 385760, 416030);
            Create_Package(236, 453150, 453160, 453220);
            Create_Package(237, 519340, 519350, 522770);
            Create_Package(238, 691410, 691420, 696590);
            Create_Package(239, 705800, 705810, 710980);
            Create_Package(240, 860480, 860490, 865610);
            Create_Package(241, 931660, 931670, 938120);
            Create_Package(242, 1159280, 1159290, 1171420);
            Create_Package(243, 1249380, 1249390, 1261450);

            //Egyptian God Armor Set
            Create_Package(244, 401770, 401780, 000000);
            Create_Package(245, 401840, 401850, 422990);
            Create_Package(246, 401910, 401920, 423020);
            Create_Package(247, 401980, 401990, 423060);
            Create_Package(248, 402050, 402060, 423080);
            Create_Package(249, 402120, 402130, 000000);
            Create_Package(250, 402190, 402200, 423140);
            Create_Package(251, 402260, 402270, 423180);
            Create_Package(252, 402330, 402340, 000000);
            Create_Package(253, 402400, 402410, 423230);
            Create_Package(254, 402470, 402480, 416030);
            Create_Package(255, 454950, 454960, 455020);
            Create_Package(256, 520910, 520920, 520920);
            Create_Package(257, 692910, 692920, 696590);
            Create_Package(258, 707300, 707310, 711210);
            Create_Package(259, 861980, 861990, 865830);
            Create_Package(260, 934280, 934290, 938450);
            Create_Package(261, 1163060, 1163070, 1171820);
            Create_Package(262, 1253160, 1253170, 1261850);

            //Scouter's Armor Set
            Create_Package(263, 385820, 385830, 416040);
            Create_Package(264, 385890, 385900, 416080);
            Create_Package(265, 385960, 385970, 416120);
            Create_Package(267, 386030, 386040, 416160);
            Create_Package(267, 386100, 386110, 416200);
            Create_Package(269, 386170, 386180, 416240);
            Create_Package(270, 386240, 386250, 416280);
            Create_Package(271, 386310, 386320, 416320);
            Create_Package(272, 386380, 386390, 416360);
            Create_Package(273, 386450, 386460, 416400);
            Create_Package(274, 386520, 386530, 416430);
            Create_Package(275, 453230, 453240, 453300);
            Create_Package(276, 519410, 519420, 522780);
            Create_Package(277, 691480, 691490, 696600);
            Create_Package(278, 705870, 705880, 710990);
            Create_Package(279, 860550, 860560, 865620);
            Create_Package(280, 931730, 931740, 938130);
            Create_Package(281, 1159350, 1159360, 1171430);
            Create_Package(282, 1249450, 1249460, 1261460);

            //Castle Lord Armor Set
            Create_Package(283, 402540, 402550, 423270);
            Create_Package(284, 402610, 402620, 423310);
            Create_Package(285, 402680, 402690, 423350);
            Create_Package(286, 402750, 402760, 423390);
            Create_Package(287, 402820, 402830, 423430);
            Create_Package(289, 402890, 402900, 423470);
            Create_Package(290, 402960, 402970, 423510);
            Create_Package(291, 403030, 403040, 423550);
            Create_Package(292, 403100, 403110, 423590);
            Create_Package(293, 403170, 403180, 423630);
            Create_Package(294, 403240, 403250, 423660);
            Create_Package(295, 455030, 455040, 518520);
            Create_Package(296, 520980, 520990, 523010);
            Create_Package(297, 692980, 692990, 696830);
            Create_Package(298, 707370, 707380, 711220);
            Create_Package(299, 862050, 862060, 865840);
            Create_Package(300, 934350, 934360, 938460);
            Create_Package(301, 1163130, 1163140, 1171830);
            Create_Package(304, 1253230, 1253240, 1261860);

            //Falcon Armor Set
            Create_Package(305, 386590, 386600, 416440);
            Create_Package(306, 386660, 386670, 416480);
            Create_Package(307, 386730, 386740, 416520);
            Create_Package(308, 386800, 386810, 416560);
            Create_Package(309, 386870, 386880, 416600);
            Create_Package(310, 386940, 386950, 416640);
            Create_Package(311, 387010, 387020, 416680);
            Create_Package(312, 387080, 387090, 416720);
            Create_Package(313, 387150, 387160, 416760);
            Create_Package(314, 387220, 387230, 416800);
            Create_Package(315, 387290, 387300, 416830);
            Create_Package(316, 453310, 453320, 453380);
            Create_Package(317, 519480, 519490, 522790);
            Create_Package(318, 691550, 691560, 696610);
            Create_Package(319, 705940, 705950, 711000);
            Create_Package(320, 860620, 860630, 865630);
            Create_Package(321, 931800, 931810, 938140);
            Create_Package(322, 1159420, 1159430, 1171440);
            Create_Package(323, 1249520, 1249530, 1261470);

            //Demonic Armor Set
            Create_Package(324, 403310, 403320, 423670);
            Create_Package(325, 403380, 403390, 423710);
            Create_Package(326, 403450, 403460, 423750);
            Create_Package(327, 403520, 403530, 423800);
            Create_Package(328, 403590, 403600, 423830);
            Create_Package(329, 403660, 403670, 423870);
            Create_Package(330, 403730, 403740, 423910);
            Create_Package(331, 403800, 403810, 423950);
            Create_Package(332, 403870, 403880, 423990);
            Create_Package(333, 403940, 403950, 424030);
            Create_Package(334, 404010, 404020, 424060);
            Create_Package(335, 455110, 455120, 455180);
            Create_Package(336, 521050, 521060, 523020);
            Create_Package(337, 693050, 693060, 696840);
            Create_Package(338, 707440, 707450, 711230);
            Create_Package(339, 862120, 862130, 865850);
            Create_Package(340, 934420, 934430, 938470);
            Create_Package(341, 1163200, 1163210, 1171840);
            Create_Package(342, 1253300, 1253310, 1261870);

            //Elite Beetle Armor Set
            Create_Package(343, 404080, 404090, 424070);
            Create_Package(344, 404150, 404160, 424110);
            Create_Package(345, 404220, 404230, 424150);
            Create_Package(346, 404290, 404300, 424190);
            Create_Package(347, 404360, 404370, 424230);
            Create_Package(348, 404430, 404440, 424270);
            Create_Package(349, 404500, 404510, 424310);
            Create_Package(350, 404570, 404580, 424350);
            Create_Package(351, 404640, 404650, 424390);
            Create_Package(352, 404710, 404720, 424430);
            Create_Package(353, 404780, 404790, 424460);
            Create_Package(354, 455190, 455200, 455260);
            Create_Package(355, 521120, 521130, 523030);
            Create_Package(356, 693120, 693130, 696850);
            Create_Package(357, 707510, 707520, 711240);
            Create_Package(358, 862190, 862200, 865860);
            Create_Package(359, 934490, 934500, 938480);
            Create_Package(360, 1163270, 1163280, 1171850);
            Create_Package(361, 1253370, 1253380, 1261880);

            //Pegasus Armor Set
            Create_Package(362, 387360, 387370, 416840);
            Create_Package(363, 387430, 387440, 416880);
            Create_Package(364, 387500, 387510, 416920);
            Create_Package(365, 387570, 387580, 416960);
            Create_Package(366, 387640, 387650, 417000);
            Create_Package(367, 387710, 387720, 417040);
            Create_Package(368, 387780, 387790, 417080);
            Create_Package(369, 387850, 387860, 417120);
            Create_Package(370, 387920, 387930, 417160);
            Create_Package(371, 387990, 388000, 417200);
            Create_Package(372, 388060, 388070, 417230);
            Create_Package(373, 453390, 453400, 453460);
            Create_Package(374, 519550, 519560, 522800);
            Create_Package(375, 691620, 691630, 696620);
            Create_Package(378, 706010, 706020, 711010);
            Create_Package(379, 860690, 860700, 865640);
            Create_Package(380, 931870, 931880, 938150);
            Create_Package(381, 1159490, 1159500, 1171450);
            Create_Package(382, 1249590, 1249600, 1261480);

            //Bull Armor Set
            Create_Package(383, 388130, 388140, 417240);
            Create_Package(384, 388200, 388210, 417280);
            Create_Package(385, 388270, 388280, 417320);
            Create_Package(386, 388340, 388350, 417360);
            Create_Package(387, 388410, 388420, 417400);
            Create_Package(388, 388480, 388490, 417440);
            Create_Package(389, 388550, 388560, 417480);
            Create_Package(390, 388620, 388630, 417520);
            Create_Package(391, 388690, 388700, 417560);
            Create_Package(392, 388760, 388770, 417600);
            Create_Package(393, 388830, 388840, 417630);
            Create_Package(394, 453470, 453480, 453540);
            Create_Package(395, 519620, 519630, 522810);
            Create_Package(396, 691690, 691700, 696630);
            Create_Package(397, 706080, 706090, 711020);
            Create_Package(398, 860760, 860770, 865650);
            Create_Package(399, 931940, 931950, 938160);
            Create_Package(400, 1159560, 1159570, 1171460);
            Create_Package(401, 1249660, 1249670, 1261490);

            //Tiger Spirit Armor Set
            Create_Package(402, 404850, 404860, 424470);
            Create_Package(403, 404920, 404930, 424510);
            Create_Package(404, 404990, 404990, 424550);
            Create_Package(405, 405060, 405070, 424590);
            Create_Package(406, 405130, 405140, 424630);
            Create_Package(407, 405200, 405210, 424670);
            Create_Package(408, 405270, 405270, 424710);
            Create_Package(409, 405340, 405350, 424750);
            Create_Package(410, 405410, 405420, 424790);
            Create_Package(411, 405480, 405490, 424830);
            Create_Package(412, 405550, 405560, 424860);
            Create_Package(413, 455270, 455280, 455340);
            Create_Package(414, 521190, 521200, 523040);
            Create_Package(415, 693190, 693200, 696860);
            Create_Package(416, 707580, 707590, 711250);
            Create_Package(417, 862260, 862270, 865870);
            Create_Package(418, 934560, 934570, 938490);
            Create_Package(419, 1163340, 1163350, 1171860);
            Create_Package(420, 1253440, 1253450, 1261890);

            //Felis Armor Set
            Create_Package(421, 405620, 405630, 424870);
            Create_Package(422, 405690, 405700, 424910);
            Create_Package(423, 405760, 405770, 424950);
            Create_Package(424, 405830, 405840, 424990);
            Create_Package(425, 405900, 405910, 425030);
            Create_Package(426, 405970, 405980, 425070);
            Create_Package(427, 406040, 406050, 425110);
            Create_Package(428, 406110, 406120, 425150);
            Create_Package(429, 406180, 406190, 425190);
            Create_Package(430, 406250, 406260, 425230);
            Create_Package(431, 406320, 406330, 425260);
            Create_Package(432, 455350, 455360, 455420);
            Create_Package(433, 521260, 521270, 523050);
            Create_Package(434, 693260, 693270, 696870);
            Create_Package(435, 707650, 707660, 711260);
            Create_Package(436, 862330, 862340, 865880);
            Create_Package(437, 934630, 934640, 938500);
            Create_Package(438, 1163410, 1163420, 1171870);
            Create_Package(439, 1253510, 1253520, 1261900);

            //Dragon Armor Set
            Create_Package(440, 388900, 388910, 417640);
            Create_Package(441, 388970, 388980, 417680);
            Create_Package(442, 389040, 389050, 417720);
            Create_Package(443, 389110, 389120, 417760);
            Create_Package(444, 389180, 389190, 417800);
            Create_Package(445, 389250, 389260, 417840);
            Create_Package(446, 389320, 389330, 417880);
            Create_Package(447, 389390, 389400, 417920);
            Create_Package(448, 389460, 389470, 417960);
            Create_Package(449, 389530, 389540, 418000);
            Create_Package(450, 389600, 389610, 418030);
            Create_Package(451, 453550, 453560, 453620);
            Create_Package(452, 519690, 519700, 522820);
            Create_Package(453, 691760, 691770, 696640);
            Create_Package(454, 706150, 706160, 711030);
            Create_Package(455, 860830, 860840, 865660);
            Create_Package(456, 932010, 932020, 938170);
            Create_Package(457, 1159630, 1159640, 1171470);
            Create_Package(458, 1249730, 1249740, 1261500);

            //Holy Black Armor Set
            Create_Package(459, 406390, 406400, 425270);
            Create_Package(460, 406460, 406470, 425310);
            Create_Package(461, 406530, 406540, 425350);
            Create_Package(462, 406600, 406610, 425390);
            Create_Package(463, 406670, 406680, 425430);
            Create_Package(464, 406740, 406750, 425470);
            Create_Package(465, 406810, 406820, 425510);
            Create_Package(466, 406880, 406890, 425550);
            Create_Package(467, 406950, 406960, 425590);
            Create_Package(468, 407020, 407030, 425630);
            Create_Package(469, 407090, 407100, 425660);
            Create_Package(470, 455430, 455440, 455500);
            Create_Package(471, 521330, 521340, 523060);
            Create_Package(472, 693330, 693340, 696880);
            Create_Package(473, 707720, 707730, 711270);
            Create_Package(474, 862400, 862410, 865890);
            Create_Package(475, 934700, 934710, 938510);
            Create_Package(476, 1163480, 1163490, 1171880);
            Create_Package(477, 1253580, 1253590, 1261910);

            //Celestial Armor Set
            Create_Package(478, 389670, 389680, 418040);
            Create_Package(479, 389740, 389750, 418080);
            Create_Package(480, 389810, 389820, 418120);
            Create_Package(481, 389880, 389890, 418170);
            Create_Package(482, 389950, 389960, 418200);
            Create_Package(483, 390020, 390030, 418240);
            Create_Package(484, 390090, 390100, 418280);
            Create_Package(485, 390160, 390170, 418320);
            Create_Package(486, 390230, 390240, 418360);
            Create_Package(487, 390300, 390310, 418400);
            Create_Package(488, 390370, 390380, 418430);
            Create_Package(489, 453630, 453640, 453700);
            Create_Package(490, 519760, 519770, 522830);
            Create_Package(491, 691830, 691840, 696650);
            Create_Package(492, 706220, 706230, 711040);
            Create_Package(493, 860900, 860910, 865670);
            Create_Package(494, 932080, 932090, 938180);
            Create_Package(495, 1159700, 1159710, 1171480);
            Create_Package(496, 1249800, 1249810, 1261510);

            //Peryton's Armor Set
            Create_Package(497, 407160, 407170, 425670);
            Create_Package(498, 407230, 407240, 425710);
            Create_Package(499, 407300, 407310, 425750);
            Create_Package(500, 407370, 407380, 425790);
            Create_Package(501, 407440, 407450, 425830);
            Create_Package(502, 407510, 407520, 425870);
            Create_Package(503, 407580, 407590, 425910);
            Create_Package(504, 407650, 407660, 425950);
            Create_Package(505, 407720, 407730, 425990);
            Create_Package(506, 407790, 407800, 426030);
            Create_Package(507, 407860, 407870, 426060);
            Create_Package(508, 455510, 455520, 455580);
            Create_Package(509, 521400, 521410, 523070);
            Create_Package(510, 693400, 693410, 696890);
            Create_Package(511, 707790, 707800, 711280);
            Create_Package(512, 862470, 862480, 865900);
            Create_Package(513, 934770, 934780, 938520);
            Create_Package(514, 1163550, 1163560, 1171890);
            Create_Package(515, 1253650, 1253660, 1261920);

            //Meister Clan Armor Set
            Create_Package(516, 390440, 390450, 418440);
            Create_Package(517, 390510, 390520, 418480);
            Create_Package(518, 390580, 390590, 418520);
            Create_Package(519, 390650, 390660, 418560);
            Create_Package(520, 390720, 390730, 418600);
            Create_Package(521, 390790, 390800, 418640);
            Create_Package(522, 390860, 390870, 418680);
            Create_Package(523, 390930, 390940, 418720);
            Create_Package(524, 391000, 391010, 418760);
            Create_Package(525, 391070, 391080, 418800);
            Create_Package(526, 391140, 391150, 418830);
            Create_Package(527, 453710, 453720, 453780);
            Create_Package(528, 519830, 519840, 522840);
            Create_Package(529, 691900, 691910, 696660);
            Create_Package(530, 706290, 706300, 711050);
            Create_Package(531, 860970, 860980, 865680);
            Create_Package(532, 932150, 932160, 938190);
            Create_Package(533, 1159770, 1159780, 1171490);
            Create_Package(534, 1249870, 1249880, 1261520);

            //Grand Chase Armor Set
            Create_Package(535, 391210, 391220, 418840);
            Create_Package(536, 391280, 391290, 418880);
            Create_Package(537, 391350, 391360, 418920);
            Create_Package(538, 391420, 391430, 418960);
            Create_Package(539, 391490, 391500, 419000);
            Create_Package(540, 391560, 391570, 419040);
            Create_Package(541, 391630, 391640, 419080);
            Create_Package(542, 391700, 391710, 419120);
            Create_Package(543, 391770, 391780, 419160);
            Create_Package(544, 391840, 391850, 419200);
            Create_Package(545, 391910, 391920, 419230);
            Create_Package(546, 453790, 453800, 453860);
            Create_Package(547, 519900, 519910, 522850);
            Create_Package(548, 691970, 691980, 696670);
            Create_Package(549, 706360, 706370, 711060);
            Create_Package(550, 861040, 861050, 865690);
            Create_Package(551, 932220, 932230, 938200);
            Create_Package(552, 1159840, 1159850, 1171500);
            Create_Package(553, 1249940, 1249950, 1261530);

            //Crusader Armor Set
            Create_Package(554, 407930, 407940, 426070);
            Create_Package(555, 408000, 408010, 426110);
            Create_Package(556, 408070, 408080, 426150);
            Create_Package(557, 408140, 408150, 426190);
            Create_Package(558, 408210, 408220, 426230);
            Create_Package(559, 408280, 408290, 426270);
            Create_Package(560, 408350, 408360, 426310);
            Create_Package(561, 408420, 408430, 426350);
            Create_Package(562, 408490, 408500, 426390);
            Create_Package(563, 408560, 408570, 426430);
            Create_Package(564, 408630, 408640, 000000);
            Create_Package(565, 455590, 455600, 455660);
            Create_Package(566, 521470, 521480, 523080);
            Create_Package(567, 693470, 693480, 696900);
            Create_Package(568, 707860, 707870, 711290);
            Create_Package(579, 862540, 862550, 865910);
            Create_Package(570, 934840, 934850, 938530);
            Create_Package(571, 1163620, 1163630, 1171900);
            Create_Package(572, 1253720, 1253730, 1261930);

            //Ascalon Armor Set
            Create_Package(573, 408700, 408710, 426470);
            Create_Package(574, 408770, 408780, 426510);
            Create_Package(575, 408840, 408850, 426550);
            Create_Package(576, 408910, 408920, 426590);
            Create_Package(577, 408980, 408990, 426630);
            Create_Package(578, 409050, 409060, 426670);
            Create_Package(589, 409120, 409130, 426710);
            Create_Package(580, 409190, 409200, 426750);
            Create_Package(581, 409260, 409270, 426790);
            Create_Package(582, 409330, 409340, 426830);
            Create_Package(583, 409400, 409410, 426860);
            Create_Package(584, 455670, 455680, 455740);
            Create_Package(585, 521540, 521550, 523090);
            Create_Package(586, 693540, 693550, 696910);
            Create_Package(587, 707930, 707940, 711300);
            Create_Package(588, 862610, 862620, 865920);
            Create_Package(589, 934910, 934920, 938540);
            Create_Package(590, 1163690, 1163700, 1171910);
            Create_Package(591, 1253790, 1253800, 1261940);

            //Valkyrie Armor Set
            Create_Package(592, 391980, 391990, 419240);
            Create_Package(593, 392050, 392060, 419280);
            Create_Package(594, 392120, 392130, 419320);
            Create_Package(595, 392190, 392200, 419360);
            Create_Package(596, 392260, 392270, 419400);
            Create_Package(597, 392330, 392340, 419440);
            Create_Package(598, 392400, 392410, 419480);
            Create_Package(599, 392470, 392480, 419520);
            Create_Package(600, 392540, 392550, 419560);
            Create_Package(601, 392610, 392620, 419600);
            Create_Package(602, 392680, 392690, 419630);
            Create_Package(603, 453870, 453880, 453940);
            Create_Package(604, 519970, 519980, 522860);
            Create_Package(605, 692040, 692050, 696680);
            Create_Package(606, 706430, 706440, 711070);
            Create_Package(607, 861110, 861120, 865700);
            Create_Package(608, 932290, 932300, 938210);
            Create_Package(609, 1163690, 1159920, 1171510);
            Create_Package(610, 1250010, 1250020, 1261540);

            //Gabriel Musketeer Armor Set
            Create_Package(611, 392750, 392760, 419640);
            Create_Package(612, 392820, 392830, 419680);
            Create_Package(613, 392890, 392900, 419720);
            Create_Package(614, 392960, 392970, 419760);
            Create_Package(615, 393030, 393040, 419800);
            Create_Package(616, 393100, 393110, 419840);
            Create_Package(617, 393170, 393180, 419880);
            Create_Package(618, 393240, 393250, 419920);
            Create_Package(619, 393310, 393320, 419960);
            Create_Package(620, 393380, 393390, 420000);
            Create_Package(621, 393450, 393460, 420030);
            Create_Package(622, 453950, 453960, 454020);
            Create_Package(623, 520040, 520050, 522870);
            Create_Package(624, 692110, 692120, 696690);
            Create_Package(625, 706500, 706510, 711080);
            Create_Package(626, 861180, 861190, 865710);
            Create_Package(627, 932360, 932370, 938220);
            Create_Package(628, 1159980, 1159920, 1171520);
            Create_Package(629, 1250080, 1250090, 1261550);

            //Captain Pilot Armor Set
            Create_Package(630, 409470, 409480, 426870);
            Create_Package(631, 409540, 409550, 426910);
            Create_Package(632, 409610, 409620, 426950);
            Create_Package(633, 409680, 409690, 426990);
            Create_Package(634, 409750, 409760, 427030);
            Create_Package(635, 409820, 409830, 427070);
            Create_Package(636, 409890, 409900, 427110);
            Create_Package(637, 409960, 409970, 427150);
            Create_Package(638, 410030, 410040, 427190);
            Create_Package(639, 410100, 410110, 427230);
            Create_Package(640, 410170, 410180, 428060);
            Create_Package(641, 455750, 455760, 455820);
            Create_Package(642, 521610, 521620, 523100);
            Create_Package(643, 693610, 693620, 696920);
            Create_Package(644, 708000, 708010, 711310);
            Create_Package(645, 862680, 862690, 865930);
            Create_Package(646, 934980, 934990, 938550);
            Create_Package(647, 1163760, 1163770, 1171920);
            Create_Package(648, 1253860, 1253870, 1261950);

            //Angelic White Armor Set
            Create_Package(649, 410240, 410250, 427270);
            Create_Package(650, 410310, 410320, 427310);
            Create_Package(651, 410380, 410390, 427350);
            Create_Package(652, 410450, 410460, 427390);
            Create_Package(653, 410520, 410530, 427430);
            Create_Package(654, 410590, 410600, 427470);
            Create_Package(655, 410660, 410670, 427510);
            Create_Package(656, 410730, 410740, 427550);
            Create_Package(657, 410800, 410810, 427590);
            Create_Package(658, 410870, 410880, 427630);
            Create_Package(659, 410940, 410950, 427660);
            Create_Package(660, 455830, 455840, 455900);
            Create_Package(661, 521680, 521690, 523110);
            Create_Package(662, 693680, 693690, 696930);
            Create_Package(663, 708070, 708080, 711320);
            Create_Package(664, 862750, 862760, 865940);
            Create_Package(665, 935050, 935060, 938560);
            Create_Package(666, 1163830, 1163840, 1171930);
            Create_Package(667, 1253930, 1253940, 1261960);

            //Iron Mask Armor Set
            Create_Package(668, 393520, 393530, 420040);
            Create_Package(669, 393590, 393600, 420080);
            Create_Package(670, 393660, 393670, 420120);
            Create_Package(671, 393730, 393740, 420160);
            Create_Package(672, 393800, 393810, 420200);
            Create_Package(673, 393870, 393880, 420240);
            Create_Package(674, 393940, 393950, 420280);
            Create_Package(675, 394010, 394020, 420320);
            Create_Package(676, 394080, 394090, 420360);
            Create_Package(677, 394150, 394160, 420400);
            Create_Package(678, 394220, 394230, 420430);
            Create_Package(679, 454030, 454040, 454100);
            Create_Package(680, 520110, 520120, 522880);
            Create_Package(681, 692180, 692190, 696700);
            Create_Package(682, 706570, 706580, 711090);
            Create_Package(683, 861250, 861260, 865720);
            Create_Package(684, 932430, 932440, 938230);
            Create_Package(685, 1160050, 1160060, 1171530);
            Create_Package(686, 1250150, 1250160, 1261560);

            //General's Armor Set
            Create_Package(687, 394290, 394300, 420440);
            Create_Package(688, 394360, 394370, 420480);
            Create_Package(689, 394430, 394440, 420520);
            Create_Package(690, 394500, 394510, 420560);
            Create_Package(691, 394570, 394580, 420600);
            Create_Package(692, 394640, 394650, 420640);
            Create_Package(693, 394710, 394720, 420680);
            Create_Package(694, 394780, 394790, 420720);
            Create_Package(695, 394850, 394860, 420760);
            Create_Package(696, 394920, 394930, 420800);
            Create_Package(697, 394990, 395000, 420830);
            Create_Package(698, 454110, 454120, 454180);
            Create_Package(699, 520180, 520190, 522890);
            Create_Package(700, 692250, 692260, 696710);
            Create_Package(701, 706640, 706650, 711100);
            Create_Package(702, 861320, 861330, 865730);
            Create_Package(703, 932500, 932510, 938240);
            Create_Package(704, 1160120, 1160130, 1171540);
            Create_Package(705, 1250220, 1250230, 1261570);

            //Noble's Armor Set
            Create_Package(706, 411010, 411020, 427670);
            Create_Package(707, 411080, 411090, 427710);
            Create_Package(708, 411150, 411160, 427750);
            Create_Package(709, 411220, 411230, 427790);
            Create_Package(710, 411290, 411300, 427830);
            Create_Package(711, 411360, 411370, 427870);
            Create_Package(712, 411430, 411440, 427910);
            Create_Package(713, 411500, 411510, 427950);
            Create_Package(714, 411570, 411580, 427990);
            Create_Package(715, 411640, 411650, 428030);
            Create_Package(716, 411710, 411720, 000000);
            Create_Package(717, 455910, 455920, 455980);
            Create_Package(718, 521750, 521760, 523120);
            Create_Package(719, 693750, 693760, 696940);
            Create_Package(720, 708140, 708150, 711330);
            Create_Package(721, 862820, 862830, 865950);
            Create_Package(722, 935120, 935130, 938570);
            Create_Package(723, 1163900, 1163910, 1171940);
            Create_Package(724, 1254000, 1254010, 1261970);

            //Baldr Armor Set
            Create_Package(725, 411780, 411790, 428070);
            Create_Package(726, 411850, 411860, 428110);
            Create_Package(727, 411920, 411930, 428150);
            Create_Package(728, 411990, 412000, 428190);
            Create_Package(729, 412060, 412070, 428230);
            Create_Package(730, 412130, 412140, 428270);
            Create_Package(731, 412200, 412210, 428310);
            Create_Package(732, 412270, 412280, 428350);
            Create_Package(733, 412340, 412350, 428390);
            Create_Package(734, 412410, 412420, 428430);
            Create_Package(735, 412480, 412490, 428460);
            Create_Package(736, 455990, 456000, 456060);
            Create_Package(737, 521820, 521830, 523130);
            Create_Package(738, 693820, 693830, 696950);
            Create_Package(739, 708210, 708220, 711340);
            Create_Package(740, 862890, 862900, 865960);
            Create_Package(741, 935190, 935200, 938580);
            Create_Package(742, 1163970, 1163980, 1171950);
            Create_Package(743, 1254070, 1254080, 1261980);
        }

        public static void SendPackageGP(WriteBuffer pw,Session p, uint _itemid, uint _itemuid, byte itemtype,int quantity,bool flag = false)
        {   
            pw.UInt(_itemid);
            pw.Int(1);
            pw.UInt(_itemuid);
            pw.Int(quantity);
            pw.Int(quantity);
            pw.Short(0);
            pw.HexArray("FF FF 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            if (flag)
            {
                p.Inventory.UpdateItem(p.Account.Login, _itemid, quantity, _itemuid);
            }
            else
            {
                p.Inventory.AddItem(p.Account.Login, _itemid, quantity, _itemuid);
            }
        }

        public static void SendPackageVP(WriteBuffer pw, Session p, uint _itemid, uint _itemuid, byte itemtype, int quantity,int valuePrice, bool flag = false,byte gradeid =0)
        {
            pw.UInt(_itemid);
            pw.Int(1);
            pw.UInt(_itemuid);
            pw.Int(quantity);
            pw.Int(quantity);
            pw.Byte(0);
            pw.Byte(gradeid);
            pw.HexArray("00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            pw.Byte(itemtype);
            pw.HexArray("00 00 00 00 00 00 00 00 00 00 00");
            pw.Byte(p.Account.CurrentCharacter);
            pw.HexArray("00 00 00 00 00 00 00 00 FF FF FF 9D");
            pw.Int((p.Account.VirtualPoints) - (valuePrice));
            pw.Int(0);
            pw.Int(valuePrice);
            pw.Byte(0);
            if (flag)
            {
                p.Inventory.UpdateItem(p.Account.Login, _itemid, quantity, _itemuid);
            }
            else
            {
                p.Inventory.AddItem(p.Account.Login, _itemid, quantity, _itemuid, gradeid);
            }
        }

    }
}
