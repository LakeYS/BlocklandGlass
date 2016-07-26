function GlassModManagerGui::renderHome(%data) {
  %container = new GuiSwatchCtrl() {
    horizSizing = "right";
    vertSizing = "bottom";
    color = "0 0 0 0";
    position = "0 0";
    extent = "505 498";
  };


  GlassModManagerGui_MainDisplay.deleteAll();
  GlassModManagerGui_MainDisplay.add(%container);
  GlassModManagerGui_MainDisplay.extent = %container.extent;

  for(%i = 0; %i < %data.length; %i++) {
    %dlg = %data.value[%i];

    %body = GlassModManagerGui::createNewUploadsDialog(%dlg.uploads, %dlg.updates);

    %container.add(%body);

    %body.setVisible(1);

    %body.text.setVisible(1);
    %body.text.forceReflow();

    %body.verticalMatchChildren(0, 10);
  }

  %container.verticalMatchChildren(498, 0);
  GlassModManagerGui_MainDisplay.setVisible(true);
  GlassModManagerGui_MainDisplay.verticalMatchChildren(498, 10);
  GlassModManagerGui_MainDisplay.getGroup().scrollToTop();
}

function GlassModManagerGui::createNewUploadsDialog(%uploads, %updates) {
  %body = new GuiSwatchCtrl() {
    horizSizing = "right";
    vertSizing = "bottom";
    color = "255 255 255 255";
    position = "10 10";
    extent = "485 10";
  };

  %text = "<font:verdana bold:13>Hey there!<br><br><font:verdana:13>We've got some new uploads for you!<br><br>";

  for(%i = 0; %i < %uploads.length; %i++) {
    %u = %uploads.value[%i];
    %name = %u.name;
    %id = %u.id;
    %author = %u.author;

    %text = %text @ "<font:verdana bold:14>  +<font:verdana:13> <a:glass://aid-" @ %id @ ">" @ %name @ "</a> by <font:verdana bold:13>" @ %author @ "<br>";

  }

  if(%updates.length > 0) {
    %text = %text @ "<br><font:verdana:13>On top of that, there's been a few recent updates<br><br>";

    for(%i = 0; %i < %updates.length; %i++) {
      %u = %updates.value[%i];
      %name = %u.name;
      %id = %u.id;
      %version = %u.version;

      %text = %text @ "<font:verdana bold:14>  +<font:verdana:13> <a:glass://aid-" @ %id @ ">" @ %name @ "</a> to version <font:verdana bold:13>" @ %version @ "<br>";

    }
  }

  %text = %text @ "<br><br>- ModBot<br><font:verdana:10><just:right>6/20/2016 2:30pm";

  %textml = new GuiMLTextCtrl() {
    horizSizing = "right";
    vertSizing = "bottom";
    profile = "GlassModManagerMLProfile";
    text = %text;
    position = "10 10";
    extent = "465 0";
    minextent = "0 0";
    autoResize = true;
  };

  %modbot = new GuiBitmapCtrl() {
    profile = "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "365 10";
    extent = "107 200";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    bitmap = "Add-Ons/System_BlocklandGlass/image/modbot";
    wrap = "0";
    lockAspectRatio = "0";
    alignLeft = "0";
    alignTop = "0";
    overflowImage = "0";
    keepCached = "0";
    mColor = "100 100 100 100";
    mMultiply = "1";
  };

  %body.add(%textml);
  %body.add(%modbot);
  %body.text = %textml;

  return %body;
}
