<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Ajax.aspx.cs" Inherits="Tests_Ajax" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="sm" runat="server" />
    <ajaxtoolkit:accordion ID="MyAccordion" runat="server" 
                           SelectedIndex="0" 
                           HeaderCssClass="accordionHeader" 
                           ContentCssClass="accordionContent" 
                           FadeTransitions="true" 
                           FramesPerSecond="40" 
                           TransitionDuration="250"
                           AutoSize="None" onload="LoadTest">
     <Panes>
          <ajaxToolkit:AccordionPane ID = "AccordionPane1" runat = "server">
          <Header>
            <a href = "" onclick = "return false;" class = "accordionLink"> Section 1</a>
          </Header>
          <Content>
            <p>This book is about to be release on a very difficult subject to find good detailed info on.</p>
            <p><img align = "left" alt = "" border = "0" src = "http://www.joeon.net/blogfiles/DOTNETInternational.jpg"></p>
            <p class = "MsoNormal"><b><font color = "#000000" face = "Arial" size = "2">
            <span style = "font-weight: bold; font-size: 10pt; color: navy; font-family: Arial">
            .NET Internationalization: The Developer's Guide to Building Global Windows and Web Applications
            </span></font></b></p>
            <p class = "MsoNormal"><font color = "#000000" face = "Arial" size = "2"><span style = "font-size: 10pt; color: navy; font-family: Arial">
            By Smith-Ferrier, Guy
            </span></font></p>
            <p class = "MsoNormal"><font color = "#000000" face = "Arial" size = "2"><span style = "font-size: 10pt; color: navy; font-family: Arial">
            Published by Addison-Wesley
            </span></font></p>
            <p class = "MsoNormal"><font color = "#000000" face = "Arial" size = "2"><span style = "font-size: 10pt; color: navy; font-family: Arial">
            Series: Microsoft .NET Development Series
            </span></font></p>
            <p class = "MsoNormal"><font color = "#000000"><font face = "Arial" size = "2"><span style = "font-size: 10pt; color: navy; font-family: Arial">
            ISBN: 0321341384; Published: 
            </span></font>
            <font face = "Arial" size = "2"><span style = "font-size: 10pt; color: navy; font-family: Arial">
            8/11/2006
            </span></font><font face = "Arial"size = "2">
            <span style = "font-size: 10pt; color: navy; font-family: Arial">
            ; Copyright 2007; Pages: T; Edition: 1
            </span></font></font></p>
            <p class = "MsoNormal"><font color = "navy" face = "Arial" size = "2">
            <span style = "font-size: 10pt; color: navy; font-family: Arial">
            <a href = "http://www.awprofessional.com/bookstore/product.asp?isbn=0321341384&amp;rl=1" target = "_blank"><font color = "#000000">http://www.awprofessional.com/bookstore/product.asp?isbn=0321341384&amp;rl=1</font></a> </span></font></p>
            <p class = "MsoNormal"><font color = "navy" face = "Arial" size = "2">
            <span style = "font-size: 10pt; color: navy; font-family: Arial">
            <a href = "http://www.joeon.net/blogfiles/SmithFerrier.pdf">You can click HERE for a sample chapter. </a></span></font></p>
          </Content>
        </ajaxToolkit:AccordionPane>

        <ajaxToolkit:AccordionPane ID = "AccordionPane2" runat = "server">
          <Header>
            <a href = "" onclick = "return false;" class = "accordionLink"> Section 2</a>
          </Header>
          <Content>
            <p>
            <img align = "left" alt = "Clown" border = "0" src = "http://www.joeon.net/blogfiles/Clown.jpg">Last week
            we but a form up on www.asp.net so that folks could send requests for How-Do-I video topics. Today I
            recieved the folowing thoughtful feedback from a values customer.</p>
            <p>"Make new versions of all the videos without the ridiculous, yet highly annoying mouse click sounds...:-(((."</p>
            <p>To whoever the anonymous poster of that comment was, I replyâ€¦.</p>
            <p>Dear Sir, thanks for your feedback. Unfortunatly, I find it necesaary to use the mouse during the
            presentations, I will however begin trainer is the secret art of â€œstealth
            mousing.&nbsp;<img src = "http://www.joeon.net/blogfiles/smile1.gif">&nbsp;</p>
            <p>To the hundreds of other folks who have taken the time to send meaningful suggestions and feedback.</p>
            <p>MANY THANKS!</p>
            <p>I'm glad that you find the videos valuable and that you are liking asp.</p>
            <p>I'll be blogging about the suggestions. Please keep them coming.</p>
            <p>Joe</p>
          </Content>
        </ajaxToolkit:AccordionPane>

        <ajaxToolkit:AccordionPane ID = "AccordionPane3" runat = "server">
          <Header>
            <a href = "" onclick = "return false;" class = "accordionLink"> Section 3</a>
          </Header>
          <Content>
            <p><img align = "left" alt = "Bill_Hilf" border = "0" src = "http://www.joeon.net/blogfiles/Bill_Hilf.gif">It's
            <a href = "http://www.microsoft.com/technet/community/tnradio/bio/billhilf.mspx" target = "_blank"> Bill
            Hilf, Microsoft General Manager of Platform Strategy</a>.</p>
            <p> There was allot of press over the last few weeks about the
            <a href = "http://news.com.com/2100-1014_3-6086044.html" target = "_blank">sudden departure of Martin Taylor. </a></p>
            <p>I've had several questions about what this means as Martin was influential in Microsoft strategy and close to Steve Ballmer.</p>
            <p>In actuality, Martin moved out of platform strategy some time ago and Bill's been the man for a number of months already.</p
            <p>I had several conversations with Martin and he is a very bright guy.</p>
            <p>But I've had lots of conversations with Bill and he is a REALLY bright guy. (In fact Iâ€™ll be on stage with Bill this weekend at the Microsoft Global Technology Exchange.)</p>
            <p>I almost went to work for Bill last year but he really needed me in Redmond and I just wasnâ€™t ready to move yet.</p>
            <p>So keep your eye out for Bill. In the wake of Bill's announcement a few weeks ago were all looking to see how Microsoft thought leadership re-aligns.</p>
          </Content>
        </ajaxToolkit:AccordionPane>

        <ajaxToolkit:AccordionPane ID = "AccordionPane4" runat = "server">
          <Header>
            <a href = "" onclick = "return false;" class = "accordionLink"> Section 4</a>
          </Header>
          <Content>
            <p>
            <font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">
            <img align = "left" alt = "MurachCS2005" border = "0" src = "http://www.joeon.net/blogfiles/MurachCS2005.jpg"></span></font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">I read A LOT of developer books. Many I like, many I loath. </span></font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial"></span> </font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">If I were going to
            teach a course on C#, I would probably choose Murachâ€™s C# 2005 as the
            textbook.&nbsp;</span></font><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">&nbsp;</span></font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">Unlike most technical
            books, this book is writing as a training resource. So while it makes a great â€œtextbookâ€, itâ€™s a â€œHow Do
            focused learning resource. </span></font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial"></span> </font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">But UNLIKE a lot of
            textbooks, it is easy to read (and therefore learn from), itâ€™s up-to-date, including detailed coverage of
            the latest C# 2.0 language constructs, and it has a commercial programming feel so itâ€™s goal is not simply
            to provide academic coverage of the programming language, but rather how to write good code with
            C#</span></font><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">&nbsp;</span></font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">Wanna learn C# ? Wanna
            learn more about C#? Want a great desk reference for C#? </span> </font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial"></span> </font></p>
            <p><font face = "Arial" size = "2"><span style = "font-size: 10pt; font-family: Arial">Buying this book
            should be a no-brainer !</span></font></p>
          </Content>
        </ajaxToolkit:AccordionPane>
     </Panes>
     </ajaxtoolkit:accordion>
</asp:Content>
