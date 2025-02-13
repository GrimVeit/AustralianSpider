using System;

public class AvatarPresenter
{
    private AvatarModel avatarModel;
    private AvatarView avatarView;

    public AvatarPresenter(AvatarModel avatarModel, AvatarView avatarView)
    {
        this.avatarModel = avatarModel;
        this.avatarView = avatarView;
    }

    public void Initialize()
    {
        ActivateEvents();

        avatarModel.Initialize();
        avatarView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        avatarModel.Dispose();
        avatarView.Dispose();
    }


    private void ActivateEvents()
    {
        avatarView.OnSubmitAvatar += avatarModel.SubmitAvatar;
        avatarView.OnChooseAvatar += avatarModel.ChooseAvatar;

        avatarModel.OnSelectIndex += avatarView.Select;
        avatarModel.OnDeselectIndex += avatarView.Deselect;
    }

    private void DeactivateEvents()
    {
        avatarView.OnSubmitAvatar -= avatarModel.SubmitAvatar;
        avatarView.OnChooseAvatar -= avatarModel.ChooseAvatar;

        avatarModel.OnSelectIndex -= avatarView.Select;
        avatarModel.OnDeselectIndex -= avatarView.Deselect;
    }

    #region Input

    public event Action<int> OnGetAvatar
    {
        add { avatarModel.OnSubmitChoose += value; }
        remove { avatarModel.OnSubmitChoose -= value; }
    }

    #endregion
}
