//================================================================
// Functions to create serializable GUI elements
//================================================================

//================================
// Common Functions
//================================

function GlassModManagerGui::setPane(%pane) {
  //somewhat an antiquated function
  //pane 1 is all dynamic content
  //pane 3 is "My Add-ons"
  //pane 4 is Colorsets
  //pane 5 is settings

  for(%a = 0; %a < 5; %a++) {
    if(%a == 1) continue;

    %obj = "GlassModManagerGui_Pane" @ %a+1;
    %obj.setVisible(false);
  }

  if(%pane == 3) {
    GlassModManagerGui::setLoading(true);
    GlassModManager.populateMyAddons();
  }

  if(%pane == 4) {
    GlassModManager::populateColorsets();
  }

  %obj = "GlassModManagerGui_Pane" @ %pane;
  %obj.setVisible(true);
}

function GlassModManagerGui::loadContext(%context) {
  //contexts are essentially just different starting points
  //for the dynamic guis
  //home, addons, build

  if(%context $= "home") {
    GlassModManager.loadHome();
  }
}

function GlassModManagerGui::setLoading(%bool) {
  if(%bool) {
    //%parent = GlassModManagerGui_LoadingAnimation.getGroup();
    //%parent.bringToFront(GlassModManagerGui_LoadingAnimation);

    GlassModManagerGui_LoadingAnimation.setVisible(true);
    GlassModManagerGui_LoadingAnimation.frame = 1;

    GlassModManager.animationTick();
  } else {
    GlassModManagerGui_LoadingAnimation.setVisible(false);
    cancel(GlassModManagerGui_LoadingAnimation.schedule);
  }
}

function GlassModManagerGui::animationTick(%this) {
  %obj = GlassModManagerGui_LoadingAnimation;
  cancel(%obj.schedule);

  %obj.frame++;
  if(%obj.frame > 22) {
    %obj.frame = 1;
  }
  GlassModManagerGui_LoadingAnimation.setBitmap("Add-Ons/System_BlocklandGlass/image/loading_animation/" @ %obj.frame @ ".png");
  %obj.schedule = %this.schedule(100, "animationTick");
}

function GlassModManagerGui::setProgress(%float, %text) {
  if(%float $= "" || isObject(%float)) {
    GlassModManagerGui_Window.extent = "675 550";
    GlassModManagerGui_ProgressBar.setVisible(false);
  } else {
    GlassModManagerGui_Window.extent = "675 585";
    GlassModManagerGui_ProgressBar.setVisible(true);

    GlassModManagerGui_ProgressBar.setValue(%float);
    GlassModManagerGui_ProgressBar.getObject(0).setText(%text);
  }
}

//================================
// Gui Classes
//================================

function GlassModManagerGui_AddonButton::onMouseEnter(%this) {
  %swatch = %this.swatch;
  if(%swatch.ocolor $= "") %swatch.ocolor = %swatch.color;

  %swatch.color = vectoradd(%swatch.color, "20 20 20");
}

function GlassModManagerGui_AddonButton::onMouseLeave(%this) {
  %swatch = %this.swatch;
  if(%swatch.ocolor $= "") %swatch.ocolor = %swatch.color;

  %swatch.color = %swatch.ocolor;
}

function GlassModManagerGui_AddonButton::onMouseDown(%this) {
  %obj = GlassModManagerGui::fetchAndRenderAddon(%this.aid);
  %obj.action = "render";
}

function GlassModManagerGui_AddonButton::onAdd(%this) {
  %this.extent = %this.swatch.extent;
  %this.position = "0 0";
}



function GlassModManagerGui_AddonDownloadButton::onMouseEnter(%this) {
  %swatch = %this.swatch;
  if(%swatch.ocolor $= "") %swatch.ocolor = %swatch.color;

  %swatch.color = "255 255 255";
}

function GlassModManagerGui_AddonDownloadButton::onMouseLeave(%this) {
  %swatch = %this.swatch;
  if(%swatch.ocolor $= "") %swatch.ocolor = %swatch.color;

  %swatch.color = %swatch.ocolor;
}

function GlassModManagerGui_AddonDownloadButton::onMouseDown(%this, %a, %pos, %c, %d, %e) {
  GlassModManagerGui::doDownloadSprite(%pos, vectorAdd(GlassModManagerGui_ProgressBar.getCanvasPosition(), GlassModManagerGui_ProgressBar.getCenter()), 100);

  %this.swatch.info.setValue("<font:quicksand-bold:16><just:center>Queued..<br><font:quicksand:14>" @ strcap(%this.branch));
  //something about redrawing the button?

  if(isObject(%this.obj))
    %tcp = GlassModManager::downloadAddon(%this.obj);
  else
    %tcp = GlassModManager::downloadAddonFromId(%this.aid);

  echo("Button! " @ %tcp.button);
  %tcp.button = %this.swatch;
  echo("Button! " @ %tcp.button);
  %tcp.branchName = %this.branch;
}

function GlassModManagerGui_AddonDownloadButton::onAdd(%this) {
  %this.extent = %this.swatch.extent;
  %this.position = "0 0";
}



function GlassModManagerGui_ForumButton::onMouseEnter(%this) {
  %swatch = %this.swatch;
  if(%swatch.ocolor $= "") %swatch.ocolor = %swatch.color;

  %swatch.color = vectoradd(%swatch.color, "20 20 20");
}

function GlassModManagerGui_ForumButton::onMouseLeave(%this) {
  %swatch = %this.swatch;
  if(%swatch.ocolor $= "") %swatch.ocolor = %swatch.color;

  %swatch.color = %swatch.ocolor;
}

function GlassModManagerGui_ForumButton::onMouseDown(%this) {
  if(%this.type $= "topic") {
    GlassForumBrowser::getAddon(%this.topic);
  } else if(%this.type $= "board") {
    GlassForumBrowser::getBoard(%this.board);
  }
}

function GlassModManagerGui_ForumButton::onAdd(%this) {
  %this.extent = %this.swatch.extent;
  %this.position = "0 0";
}



function GlassModManagerGui_ScreenshotButton::onMouseEnter(%this) {
  %swatch = %this.swatch;
  if(%swatch.ocolor $= "") %swatch.ocolor = %swatch.color;

  %swatch.color = "255 255 255";
}

function GlassModManagerGui_ScreenshotButton::onMouseLeave(%this) {
  %swatch = %this.swatch;
  if(%swatch.ocolor $= "") %swatch.ocolor = %swatch.color;

  %swatch.color = %swatch.ocolor;
}

function GlassModManagerGui_ScreenshotButton::onMouseDown(%this, %a, %pos, %c, %d, %e) {
  GlassModManagerGui::downloadAndDisplayScreenshot(%this.aid, %this.screenshotId); // TODO check to see if it's downloaded, if now download; render
}

function GlassModManagerGui_AddonScreenshotButton::onAdd(%this) {
  %this.extent = %this.swatch.extent;
  %this.position = "0 0";
}

exec("./modmanager/trending.cs");
exec("./modmanager/errorPage.cs");
exec("./modmanager/addonPage.cs");
exec("./modmanager/forumBrowser.cs");