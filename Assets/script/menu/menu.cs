using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu 
{
	private int menuID;
	private string menuName;
	private int price;
	private DataMgr.Cook cookWay;
	private DataMgr.Food useFood;
	private DataMgr.Staple useStaple;

	public menu( int ID, string name, int price, DataMgr.Cook cook, DataMgr.Food food, DataMgr.Staple staple )
	{
		menuID = ID;
		menuName = name;
		this.price = price;
		cookWay = cook;
		useFood = food;
		useStaple = staple;
	}

	public int getMenuID(){return menuID;}
	public string getMenuName(){return menuName;}
	public int getPrice(){return price;}
	public DataMgr.Cook getCookWay(){return cookWay;}
	public DataMgr.Food getUseFood(){return useFood;}
	public DataMgr.Staple getUseStaple(){return useStaple;}
}
