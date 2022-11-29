using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour
{
    [SerializeField] private List<SearchSO> allSearchs;
    [SerializeField] private Transform container;
    [SerializeField] private SearchItem template;
    
    private List<SearchSO> completedSearchs = new List<SearchSO>();

    public class OnSearchCompletedEventArgs : EventArgs
    {
        public SearchSO completedSearch;
    }

    public static event EventHandler<OnSearchCompletedEventArgs> OnSearchCompleted; 
    
    private void Start()
    {
        FillSearchs();
    }

    private void FillSearchs()
    {
        foreach (Transform t in container)
        {
            if(t.name.Equals("CardTemplate")) continue;
            
            Destroy(t.gameObject);
        }
        
        foreach (SearchSO item in allSearchs)
        {
            if(completedSearchs.Contains(item)) continue;

            SearchItem newItem = Instantiate(template, container);
            newItem.Fill(() => BuySeach(item), item);
            newItem.gameObject.SetActive(true);
        }
    }

    private void BuySeach(SearchSO search)
    {
        if(!PlayerMoney.instance.SpendCoins(search.Price)) return;
        Debug.Log("teste");
        completedSearchs.Add(search);
        allSearchs.Remove(search);
        OnSearchCompleted?.Invoke(this, new OnSearchCompletedEventArgs()
        {
            completedSearch = search
        });
        FillSearchs();
    }
}
