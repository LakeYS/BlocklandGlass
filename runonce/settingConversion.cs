if(isFile("config/BLG/client/mm.cs")) {
  echo("Pre-1.1 settings found. Converting.");

  exec("config/BLG/client/mm.cs");
  GlassSettings.update("MM::Keybind", $BLG::MM::Keybind);
  GlassSettings.update("MM::UseDefault", $BLG::MM::UseUpdaterDefault);
  GlassSettings.update("MM::Colorset", $BLG::MM::Colorset);


  // TODO cache
  // fileDelete("config/BLG/client/mm.cs");
}
