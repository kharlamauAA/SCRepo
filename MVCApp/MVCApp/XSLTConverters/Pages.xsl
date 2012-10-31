<!DOCTYPE html>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <head>
        <title>
          <xsl:value-of select="page/form/title" />
        </title>
      </head>
      <body>
        <xsl:apply-templates/>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="form">
    <xsl:apply-templates select="subtitle"/>
    <form method="post" id="mform">
      <xsl:attribute name="action">
        <xsl:value-of select="@action"/>
      </xsl:attribute>
      <xsl:apply-templates select="input"/>
      <xsl:apply-templates select="submit"/>
    </form>
  </xsl:template>

  <xsl:template match="input">
    <input type="text">
      <xsl:attribute name="placeholder">
        <xsl:value-of select="." />
      </xsl:attribute>
      <xsl:attribute name="name">
        <xsl:value-of select="." />
      </xsl:attribute>
      <xsl:attribute name="id">
        <xsl:value-of select="." />
      </xsl:attribute>
    </input>
    <br/>
  </xsl:template>

  <xsl:template match="submit">
    <input type="submit">
      <xsl:attribute name="value">
        <xsl:value-of select="." />
      </xsl:attribute>
    </input>
    <br/>
  </xsl:template>


  <xsl:template match="subtitle">
    <h2>
      <xsl:value-of select="." />
    </h2>
  </xsl:template>

</xsl:stylesheet>