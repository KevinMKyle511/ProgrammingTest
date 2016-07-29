using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PeopleTimes
{
    public int Birth { get; set; }
    public int Death { get; set; }

    public PeopleTimes()
    {

    }

    public PeopleTimes(int b, int d)
    {
        this.Birth = b;
        this.Death = d;
    }
}

public class ListMaker : MonoBehaviour
{
    int ListSize = 11;
    public GameObject ListItemPrefab;
    public Transform ListParent;
    public Text SolvedYearText;
    public Text SolvedNumberOfPeopleText;
    // Use this for initialization
	void Start ()
    {
        MakeList(ListSize);
	}

    public void FindYearMostPeopleAreAliveButtonPressed()
    {
        var list = new List<PeopleTimes>();
        for (int i = 0; i < Globals.Instance.TheList.Count; i++)
        {
            var person = new PeopleTimes(Globals.Instance.TheList[i].BirthYear, Globals.Instance.TheList[i].DeathYear);
            list.Add(person);
        }

        int yearWithMostalive = 1900;
        int highestAlive = 0;

        for (int i = 1900; i <= 2000; i++)
        {
            int totalAliveinYear = 0;
            foreach (var person in list)
            {
                if (i >= person.Birth && i <= person.Death)
                    totalAliveinYear = totalAliveinYear + 1;
            }

            if (totalAliveinYear > highestAlive)
            {
                highestAlive = totalAliveinYear;
                yearWithMostalive = i;
            }
        }
        SolvedNumberOfPeopleText.text = highestAlive.ToString();
        SolvedYearText.text = yearWithMostalive.ToString();
    }

    public void MakeNewListButtonPressed(Text ListSizeText)
    {
        //Debug.Log(ListSizeText.text);
        ListSize = int.Parse(ListSizeText.text);
        if (ListSize <= 0)
            Debug.Log("You have inputed " + ListSize + " this is not a vaild list size");
        else
            MakeList(ListSize);
    }

    void MakeList(int Size)
    {
        DestoryListChildren();
        Globals.Instance.TheList.Clear();
        for (int i = 0; i < Size; i++)
        {
            Globals.Instance.TheList.Add(MakeNewListData());
        }
        //for (int i = 0; i < Globals.Instance.TheList.Count; i++)
            //Debug.Log("Birth Year " + Globals.Instance.TheList[i].BirthYear + " Death Year " + Globals.Instance.TheList[i].DeathYear);
        PopulateScrollableList();
    }

    ListData MakeNewListData()
    {
        ListData temp = new ListData();
        temp.BirthYear = Random.Range(1900, 2000);
        temp.DeathYear = Random.Range(temp.BirthYear, 2001);
        return temp;
    }

    void PopulateScrollableList()
    {
        for (int i = 0; i < Globals.Instance.TheList.Count; i++)
        {
            GameObject temp = Instantiate(ListItemPrefab);
            temp.transform.FindChild("ListNumberText").GetComponent<Text>().text = "#"+(i + 1).ToString();
            temp.transform.FindChild("BirthYearText").GetComponent<Text>().text = Globals.Instance.TheList[i].BirthYear.ToString();
            temp.transform.FindChild("DeathYearText").GetComponent<Text>().text = Globals.Instance.TheList[i].DeathYear.ToString();
            temp.transform.SetParent(ListParent, false);
        }
    }

    void DestoryListChildren()
    {
        foreach (Transform child in ListParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

}
