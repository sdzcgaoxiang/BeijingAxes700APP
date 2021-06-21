using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionIntroSave 
{
    private static CollectionIntroSave m_Instance;
    public static CollectionIntroSave Instance
    {
        get {
            if (m_Instance == null)
                m_Instance = new CollectionIntroSave();
            return m_Instance;
        }
    }
    public struct Collection
    {
        public string imagePath;
        public string title;
        public string content;
    }
    public Dictionary<Enum_Collection, Collection> collection;
    public CollectionIntroSave()
    {
        collection = new Dictionary<Enum_Collection, Collection>();
        collection.Add(Enum_Collection.YuPei, new Collection()
        {
            imagePath = "UI/Photos/YuPei",
            title = "玉佩",
            content = "在中国，玉器从旧石器时代至今已有5000多年的历史了，它记录了人类生活，社会的变迁。这块玉佩通体晶莹，似乎是清朝道光年间款式，但却光洁如新，上面刻有胤字。"
        });
        collection.Add(Enum_Collection.ZheShan, new Collection()
        {
            imagePath = "UI/Photos/ZheShan",
            title = "折扇",
            content = "折扇，初名腰扇，滥觞于汉末，曾是王公大人的宠物。晋代，腰扇又称为叠扇，已成为上流社会男女通用的驱暑器具。此扇似乾隆画“同心训迪”图折扇，但却如才饰拂尘。"
        });
        collection.Add(Enum_Collection.Shu, new Collection()
        {
            imagePath = "UI/Photos/Shu",
            title = "史书残本",
            content = "“二十四史”以本纪、列传、表、志等形式，纵横交错，脉络贯通，反映了中国错综复杂的历史进程。此二十四史残本纸张如百年前，但字迹却如新。"
        });
        collection.Add(Enum_Collection.ZhuanKuai, new Collection()
        {
            imagePath = "UI/Photos/ZhuanKuai",
            title = "古砖",
            content = "“天安门下层座上为高10多米的红色墩台，以每块重达43千克的大砖砌成。这砖块似乎是天安门于清朝返修遗留之物，却不知为何未被发现。"
        });
    }
}
