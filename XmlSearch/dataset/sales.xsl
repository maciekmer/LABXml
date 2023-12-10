<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:template match="Sales">
    <html>
        <body>
            <table border="1">
                <tr>
                    <th>InvoiceId</th>
                    <th>Branch</th>
                    <th>City</th>
                    <th>CustomerType</th>
                    <th>Sex</th>
                    <th>ProductLine</th>
                    <th>UnitPrice</th>
                    <th>Quantity</th>
                    <th>Tax</th>
                    <th>Total</th>
                    <th>Date</th>
                    <th>Payment</th>
                    <th>CostOfGoods</th>
                    <th>Rating</th>
                </tr>
                <xsl:apply-templates select="Sale"/>
            </table>
        </body>
    </html>
</xsl:template>
<xsl:template match="Sale">
    <tr>
        <td>
            <xsl:value-of select="InvoiceId"/>
        </td>
        <td>
            <xsl:value-of select="Branch"/>
        </td>
        <td>
            <xsl:value-of select="City"/>
        </td>
        <td>
            <xsl:value-of select="CustomerType"/>
        </td>
        <td>
            <xsl:value-of select="Sex"/>
        </td>
        <td>
            <xsl:value-of select="ProductLine"/>
        </td>
        <td>
            <xsl:value-of select="UnitPrice"/>
        </td>
        <td>
            <xsl:value-of select="Quantity"/>
        </td>
        <td>
            <xsl:value-of select="Tax"/>
        </td>
        <td>
            <xsl:value-of select="Total"/>
        </td>
        <td>
            <xsl:value-of select="Date"/>
        </td>
        <td>
            <xsl:value-of select="Payment"/>
        </td>
        <td>
            <xsl:value-of select="CostOfGoods"/>
        </td>
        <td>
            <xsl:value-of select="Rating"/>
        </td>
    </tr>
</xsl:template>
</xsl:stylesheet>
