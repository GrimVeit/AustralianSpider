using System;

public class NicknamePresenter
{
    private NicknameModel nicknameModel;
    private NicknameView nicknameView;

    public NicknamePresenter(NicknameModel nicknameModel, NicknameView nicknameView)
    {
        this.nicknameModel = nicknameModel;
        this.nicknameView = nicknameView;
    }

    public void Initialize()
    {
        ActivateEvents();

        nicknameModel.Initialize();
        nicknameView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        nicknameView.Dispose();
    }

    private void ActivateEvents()
    {
        nicknameView.OnChangeNickname += nicknameModel.ChangeNickname;

        nicknameModel.OnGetNickname += nicknameView.ChangeNickname;
        nicknameModel.OnEnterRegisterLoginError += nicknameView.DisplayDescription;
    }

    private void DeactivateEvents()
    {
        nicknameView.OnChangeNickname -= nicknameModel.ChangeNickname;

        nicknameModel.OnGetNickname -= nicknameView.ChangeNickname;
        nicknameModel.OnEnterRegisterLoginError -= nicknameView.DisplayDescription;
    }

    #region Input

    public event Action<string> OnGetNickname
    {
        add { nicknameModel.OnGetNickname += value; }
        remove { nicknameModel.OnGetNickname -= value; }
    }

    public event Action OnCorrectNickname
    {
        add { nicknameModel.OnCorrectNickname += value; }
        remove { nicknameModel.OnCorrectNickname -= value; }
    }

    public event Action OnIncorrectNickname
    {
        add { nicknameModel.OnIncorrectNickname += value; }
        remove { nicknameModel.OnIncorrectNickname -= value; }
    }

    #endregion
}
