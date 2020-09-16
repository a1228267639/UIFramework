using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
//2020/05/08/15:04:58
//YZJ_UIFramework
//.cs
//杨智杰
//云笔

public class PageDialogBackgroundControl : MonoBehaviour, IPointerClickHandler
{

    private DialogAnimator mDialogAnimator;
    public DialogAnimator dialogAnimator
    {
        get { return mDialogAnimator; }
        set { mDialogAnimator = value; }
    }

    public async void Start()
    {

    }



    public void OnPointerClick(PointerEventData eventData)
    {

        HTTPManager.Instance.GetAsync<Data.Root>(@"https://api.ituring.com.cn/api/Live/TrainingPlan?page=1", response =>
        {
            if (response != null)
            {
                Data.Root result = (Data.Root)response;
                foreach(var book in result.trainingPlanItems)
                {
                    string img = $"http://file.ituring.com.cn/SmallCover/{book.coverKey}";
                    Debug.Log(img);
                    HTTPManager.Instance.GetAsync(img, dd =>
                    {
                        GameObject go = new GameObject();
                        UnityEngine.UI.RawImage  rawImage = go.AddComponent<UnityEngine.UI.RawImage>();
                        rawImage.texture = dd;
                    });
                }
                print(result.trainingPlanItems[2].memo);
            }

        });

        //HTTPManager.Instance.Get(this, "/api/Customer/GetWaitDoCount?CusID=22",
        //(progress) =>
        //{
        //    this.Log(progress);
        //}, (error) => { this.Log(error); },
        //(finish) => { this.Log(finish); });
    }
}

namespace Data
{
    public class TrainingPlanItems
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// 《Scratch 3.0少儿编程》配套视频
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int salePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int teacherId { get; set; }
        /// <summary>
        /// 童程智优
        /// </summary>
        public string teacherName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string teacherAvatarImageKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int classHour { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool showNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int baseNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int liveShowStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool published { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string publishTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool sellStopped { get; set; }
    }

    public class Pagination
    {
        /// <summary>
        /// 
        /// </summary>
        public int pageCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalItemCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool hasPreviousPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool hasNextPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isFirstPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isLastPage { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public List<TrainingPlanItems> trainingPlanItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Pagination pagination { get; set; }
    }


}