program RfCommServer;

uses
  Vcl.Forms,
  ServerMain in 'ServerMain.pas' {fmServerMain},
  Common in '..\Shared\Common.pas',
  ServerDataProcessor in 'ServerDataProcessor.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TfmServerMain, fmServerMain);
  Application.Run;
end.
