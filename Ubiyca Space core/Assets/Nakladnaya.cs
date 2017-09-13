using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nakladnaya : MonoBehaviour {

	public Text Podpis;
	public int count;
	public static Nakladnaya nakladnaya;



	private void Start()
	{
		count = 0;
		Podpis.text = "Коробок на складе: " + count;
	}


	public void AddCount(int NewCount)
	{
		count += NewCount;
		UpdateNakladnya();
	}

	public void UpdateNakladnya()
	{
		Podpis.text = "Коробок на складе: " + count;
	}



}
