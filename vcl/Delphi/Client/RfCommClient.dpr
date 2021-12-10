program RfCommClient;

uses
  Vcl.Forms,
  ClientMain in 'ClientMain.pas' {fmClientMain},
  Common in '..\Shared\Common.pas',
  ClientDataProcessor in 'ClientDataProcessor.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TfmClientMain, fmClientMain);
  Application.Run;
end.
