using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rightOrderCtrl : MonoBehaviour, iSyncOrderOption
{
	[SerializeField]
	private checkMenu orderImgCtrl;
	[SerializeField]
	private Image numberImg;
	[SerializeField]
	private Sprite[] numberList;
	[SerializeField]
	private slidAndClick orderNumCtrl;
	[SerializeField]
	private page7Ctrl pageCtrl;

	void Awake () 
	{
		orderNumCtrl.setCallBack (slidAndClick.Direction.LEFT, minusOrderNum);
		orderNumCtrl.setCallBack (slidAndClick.Direction.RIGHT, addOrderNum);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDeleteClick()
	{
		Debug.logger.Log ("OnDeleteClick");

	}

	public void addOrderNum()
	{
		pageCtrl.modifyOrderNumber (1);
	}

	public void minusOrderNum()
	{
		pageCtrl.modifyOrderNumber (-1);
	}

	private void updateNumberImg(int orderNum)
	{
		int numberIndex = orderNum - 1;
		numberImg.sprite = numberList [numberIndex];
	}


	// 增加一道餐點
	public void addOrder (int menuID)
	{

	}

	// 刪除一道餐點
	public void delOrder (int menuID)
	{
		LeanTween.scale(gameObject, Vector3.zero, 0.3f);
	}

	public void delOrder_step2()
	{
		
	}

	// 修改餐點的分數
	public void modifyOrderNumber(int orderNum)
	{
		updateNumberImg(orderNum);
	}

	// 顯示餐點
	public void showOrder(int menuID)
	{
		Sprite sprite = spriteMgr.Instance.getSprite (spriteMgr.KeyWord.WANT_ORDER, menuID);
		orderImgCtrl.setMenuImage(sprite);

		custom cus = DataMgr.Instance.getOrderingCustom ();
		Dictionary<int,int> confirmMenu = cus.getConfirmMenu ();
		updateNumberImg(confirmMenu [menuID]);
		transform.localScale = Vector3.one;
	}

	// 重置餐點資料
	public void resetOrder()
	{

	}
}
