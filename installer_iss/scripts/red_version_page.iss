[CustomMessages]
en.hint_patchNeeded=foobbar 111
en.install_patch=Patch installieren2222

[Code]
var ButtonPatchInst: TButton;
var ErrorCode: Integer;

procedure ButtonPatchInst_OnClick(Sender: TObject);
var 
    ErrorCode: Integer;
begin
    SuppressibleMsgBox('blub.', mbCriticalError, MB_OK, MB_OK);
end;

procedure ChooseREDVersionPage;
var 
    Page: TWizardPage;
    //HintPN, ProgressBarLabel: TLabel;
begin

    //Page := CreateCustomPage(wpLicense, 'Vorrausetzung', 'für das {#NAME} {#VERSION}.');
    Page := CreateCustomPage(wpSelectComponents, 'Vorrausetzung', 'für das {#NAME} {#VERSION}.');

    //HintPN := TLabel.Create(Page);
    //HintPN.Caption := CustomMessage('hint_patchNeeded');
    //HintPN.AutoSize := True;
    //HintPN.Parent := Page.Surface;

    ButtonPatchInst := TButton.Create(Page);
    ButtonPatchInst.Parent := Page.Surface; 
    ButtonPatchInst.Caption := CustomMessage('install_patch');
    ButtonPatchInst.Width := ScaleX(120);
    ButtonPatchInst.Height := ScaleY(23);
    ButtonPatchInst.OnClick := @ButtonPatchInst_OnClick;
    
end;


procedure CreateTheWizardPages;
var
  Page: TWizardPage;
  Button, FormButton: TButton;
  CheckBox: TCheckBox;
  Edit: TEdit;
  PasswordEdit: TPasswordEdit;
  Memo: TMemo;
  Lbl, ProgressBarLabel: TLabel;
  ComboBox: TComboBox;
  ListBox: TListBox;
  StaticText: TNewStaticText;
  ProgressBar: TNewProgressBar;
  CheckListBox, CheckListBox2: TNewCheckListBox;
  FolderTreeView: TFolderTreeView;
  BitmapImage, BitmapImage2, BitmapImage3: TBitmapImage;
  BitmapFileName: String;
  RichEditViewer: TRichEditViewer;
begin
  { TButton and others }

  Page := CreateCustomPage(wpWelcome, 'Custom wizard page controls', 'TButton and others');

  CheckBox := TCheckBox.Create(Page);
  CheckBox.Top := Button.Top + Button.Height + ScaleY(8);
  CheckBox.Width := Page.SurfaceWidth;
  CheckBox.Height := ScaleY(17);
  CheckBox.Caption := 'Install a RED version that requires Administrator rights each time you start it';
  //CheckBox.Checked := True;
  CheckBox.Parent := Page.Surface;


end;