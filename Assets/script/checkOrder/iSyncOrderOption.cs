using System.Collections;
using System.Collections.Generic;


interface iSyncOrderOption 
{
	// 增加一道餐點
	void addOrder (int menuID);
	// 刪除一道餐點
	void delOrder (int menuID);
	// 修改餐點的分數
	void modifyOrderNumber(int number);
	// 顯示餐點
	void showOrder(int menuID);
	// 重置餐點資料
	void resetOrder();
}
