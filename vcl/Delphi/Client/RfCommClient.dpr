program RfCommClient;

uses
  Forms,
  ClientMain in 'ClientMain.pas' {fmClientMain},
  Common in '..\Shared\Common.pas',
  ClientDataProcessor in 'ClientDataProcessor.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfmClientMain, fmClientMain);
  Application.Run;
end.
