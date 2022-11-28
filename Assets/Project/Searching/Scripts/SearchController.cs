using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour
{
    [SerializeField] private List<SearchSO> allSearchs;
    [SerializeField] private Transform container;
    [SerializeField] private SerachItem template;
    
    private List<SearchSO> completedSearchs;

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
        foreach (SearchSO item in allSearchs)
        {
            if(completedSearchs.Contains(item)) continue;

            SerachItem newItem = Instantiate(template, container);
            newItem.Fill(() => BuySeach(item));
            newItem.gameObject.SetActive(true);
        }
    }

    private void BuySeach(SearchSO search)
    {
        if(!PlayerMoney.instance.SpendCoins(search.Price)) return;
        
        completedSearchs.Add(search);
        allSearchs.Remove(search);
        OnSearchCompleted?.Invoke(this, new OnSearchCompletedEventArgs()
        {
            completedSearch = search
        });
    }
}
