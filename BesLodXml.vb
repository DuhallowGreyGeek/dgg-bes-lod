Imports System.Text
Imports System.Xml

Public Class BesLodXml
    'Object bringing together all the XML function Bessie Load uses to read the XML file containing 
    'the batch of documents.

    Public Sub New()

    End Sub

    Public Sub loadDocBatch()
        'Process the XML document - will this approach be acceptable with a large XML file?
        Dim xDoc As New XmlDocument
        xDoc.Load("C:\Users\user\Documents\Bessie_20180824\BesTestLoad.xml")

        Dim s1 As String = xDoc.SelectSingleNode("doc_batch/batch_header/batch_filename").InnerText
        frmMain.lstLoadProgress.Items.Add("_filename---> " & s1)

        Dim s2 As String = xDoc.SelectSingleNode("doc_batch/batch_header/created_date").InnerText
        frmMain.lstLoadProgress.Items.Add("_created_date---> " & s2)

        frmMain.lstLoadProgress.Items.Add("---now loop through the **documents** --- ")

        Dim documentList As XmlNodeList = xDoc.GetElementsByTagName("doc_body")
        Dim nodeText As String
        Dim nodeName As String

        For Each thisNode As XmlNode In documentList
            If (thisNode.Name = "doc_body") Then
                frmMain.lstLoadProgress.Items.Add("--New Document--")

                'Note the FirstChild and LastChild properties and the ItemOf index is 0 based

                nodeText = thisNode.ChildNodes.ItemOf(0).InnerText.ToString
                frmMain.lstLoadProgress.Items.Add("    --_/doc_filename --> " & nodeText)

                nodeName = thisNode.ChildNodes.ItemOf(1).Name.ToString
                nodeText = thisNode.ChildNodes.ItemOf(1).InnerText.ToString
                frmMain.lstLoadProgress.Items.Add("    --_/" & nodeName & " --> " & nodeText)


                nodeText = thisNode.ChildNodes.ItemOf(2).InnerText.ToString
                frmMain.lstLoadProgress.Items.Add("    --_/doc_title --> " & nodeText)


                Console.Write(thisNode.LastChild.InnerText.ToString)
                'Now get the review or revision data
                Dim revList As XmlNodeList = xDoc.GetElementsByTagName("doc_rev")
                For Each thisReview As XmlNode In revList
                    frmMain.lstLoadProgress.Items.Add("     --New Review--")
                    'Note the FirstChild and LastChild properties and the ItemOf index is 0 based
                    Dim j As Integer

                    For j = 0 To thisReview.ChildNodes.Count - 1
                        nodeName = thisReview.ChildNodes.ItemOf(j).Name.ToString
                        nodeText = thisReview.ChildNodes.ItemOf(j).InnerText.ToString
                        frmMain.lstLoadProgress.Items.Add("         --_/" & nodeName & " --> " & nodeText)
                    Next

                Next

                'nodeText = thisNode.FirstChild.InnerText.ToString
                'nodeText = thisNode.LastChild.InnerText.ToString

            End If
            If (thisNode.Name = "doc_rev") Then
                frmMain.lstLoadProgress.Items.Add("     --New Review--")

                'Note the FirstChild and LastChild properties and the ItemOf index is 0 based
                Dim j As Integer

                For j = 0 To thisNode.ChildNodes.Count
                    nodeName = thisNode.ChildNodes.ItemOf(1).Name.ToString
                    nodeText = thisNode.ChildNodes.ItemOf(1).InnerText.ToString
                    frmMain.lstLoadProgress.Items.Add("         --_/" & nodeName & " --> " & nodeText)
                Next

            End If
            frmMain.lstLoadProgress.Items.Add(" ")
        Next

        frmMain.lstLoadProgress.Items.Add(" ")
        frmMain.lstLoadProgress.Items.Add("--Number of documents found ---> " & documentList.Count.ToString)

    End Sub
End Class
