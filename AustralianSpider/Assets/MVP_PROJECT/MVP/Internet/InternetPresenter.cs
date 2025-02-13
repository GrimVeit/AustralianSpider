using System;

public class InternetPresenter
{
    private InternetModel internetModel;
    private InternetView internetView;

    public InternetPresenter(InternetModel internetModel, InternetView internetView)
    {
        this.internetModel = internetModel;
        this.internetView = internetView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }

    public void StartCheckInternet()
    {
        internetModel.StartCheckInternet();
    }

    public event Action OnInternetUnavailable
    {
        add { internetModel.OnInternetUnvailable += value; }
        remove { internetModel.OnInternetUnvailable -= value; }
    }

    public event Action OnInternetAvailable
    {
        add { internetModel.OnInternetAvailable += value;}
        remove { internetModel.OnInternetAvailable += value; }
    }
}
