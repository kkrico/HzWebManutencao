using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.UI;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Tools.Applications;

namespace HzWebManutencao.Classes
{
	/// <summary>
	/// 
	/// </summary>
	/// 

	public class CCWordApp : IDisposable
	{
		private Application oWordApplic;	        // a reference to Word application
		private Document    oDoc;					// a reference to the document
        private object      missing = System.Reflection.Missing.Value;
		
		public CCWordApp()
		{
			// activate the interface with the COM object of Microsoft Word
			oWordApplic = new Application();
		}

		// Open a file (the file must exists) and activate it
		public void Open( string strFileName)
		{
			object fileName = strFileName;
			object readOnly = false;
            object isVisible = false;

			oDoc = oWordApplic.Documents.Open(ref fileName, ref missing,ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible,ref missing,ref missing,ref missing);
			oDoc.Activate();	
		}		

		// Open a new document
        public void Open()
        {
            oDoc = oWordApplic.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            oDoc.Activate();
        }

        // Open a new document with templates
        public void OpenTemplate(string strFileName)
        {
            object fileName = strFileName;
            oDoc = oWordApplic.Documents.Add(fileName, ref missing, ref missing, ref missing);
            oDoc.Activate();
        }		

		public void Save( )
		{
            oDoc.Save();			
		}

		public void SaveAs(string strFileName )
		{
			object fileName = strFileName;
			oDoc.SaveAs(ref fileName, ref missing,ref missing, ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
		}

 		// Save the document in HTML format
		public void SaveAsHtml(string strFileName )
		{
			object fileName = strFileName;
			object Format = (int)Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;
			oDoc.SaveAs(ref fileName, ref Format,ref missing, ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
		}

        public void SaveAsPDF(string strFileName)
        {
            object fileName = strFileName;
            object Format = (int)Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
            oDoc.SaveAs(ref fileName, ref Format, ref missing, ref missing, ref missing, ref missing, ref missing,ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }

        public void Quit()
        {
            oDoc.Close();
            LiberarProcessoWord();

            //oWordApplic.Application.Quit(ref missing, ref missing, ref missing);	
            //((_Application)oWordApplic)(SaveChanges: false, OriginalFormat: false, RouteDocument: false);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(oWordApplic);
        }		

        public void Dispose() 
        { 
            LiberarProcessoWord(); 
        } 
        
        private void LiberarProcessoWord() 
        { 
            // Libera objeto relacionado ao arquivo que está sendo manipulado 
            ReleaseComObject(oDoc); 
            oDoc = null; 
            // Encerra a instância do Word
            oWordApplic.Quit(); 
            ReleaseComObject(oWordApplic); 
            oWordApplic = null; 
        } 
        
        private void ReleaseComObject(object o) 
        {   // Liberar instância do Interop 
            if (o != null) 
                System.Runtime.InteropServices .Marshal.ReleaseComObject(o); 
        }

        public void WordPrint(string strFilename)
        {
            object fileName = strFilename;
            oDoc.PrintOut(ref missing, ref missing, ref missing, ref fileName, ref missing,ref missing,ref missing,
				          ref missing,ref missing,ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }

		public void InsertText( string strText)
		{
			oWordApplic.Selection.TypeText(strText);
		}

		public void InsertLineBreak( )
		{
			oWordApplic.Selection.TypeParagraph();
		}

		public void InsertLineBreak( int nline)
		{
			for (int i=0; i<nline; i++)
				oWordApplic.Selection.TypeParagraph();
		}

		// Change the paragraph alignement
		public void SetAlignment(string strType )
		{
			switch (strType)
			{
				case "Center" :
					oWordApplic.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
				break;
				case "Left" :
					oWordApplic.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					break;
				case "Right" :
					oWordApplic.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
					break;
				case "Justify" :
					oWordApplic.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
					break;
			}
	
		}


		// if you use thif function to change the font you should call it again with 
		// no parameter in order to set the font without a particular format
		public void SetFont( string strType )
		{
			switch (strType)
			{
				case "Bold":
					oWordApplic.Selection.Font.Bold = 1;
					break;
				case "Italic":
					oWordApplic.Selection.Font.Italic = 1;
					break;
				case "Underlined":
					oWordApplic.Selection.Font.Subscript = 0;
					break;
			}
			
		}
		
		// disable all the style 
		public void SetFont( )
		{
			oWordApplic.Selection.Font.Bold = 0;
			oWordApplic.Selection.Font.Italic = 0;
			oWordApplic.Selection.Font.Subscript = 0;
		
		}

		public void SetFontName( string strType )
		{
			oWordApplic.Selection.Font.Name = strType;
			
		} 

		public void SetFontSize( int nSize )
		{
			oWordApplic.Selection.Font.Size = nSize;
			
		} 

		public void InsertPagebreak()
		{
			// VB : Selection.InsertBreak Type:=wdPageBreak
			object pBreak= (int)WdBreakType.wdPageBreak;
			oWordApplic.Selection.InsertBreak(ref pBreak );
		}

		// Go to a predefined bookmark, if the bookmark doesn't exists the application will raise an error

		public void GotoBookMark( string strBookMarkName)
		{
			// VB :  Selection.GoTo What:=wdGoToBookmark, Name:="nome"

			object Bookmark = (int)WdGoToItem.wdGoToBookmark;
			object NameBookMark = strBookMarkName;
			oWordApplic.Selection.GoTo(ref Bookmark, ref missing, ref missing,ref NameBookMark);
		}

		public void GoToTheEnd( )
		{
			// VB :  Selection.EndKey Unit:=wdStory
			object unit ;
			unit = WdUnits.wdStory ;
			oWordApplic.Selection.EndKey ( ref unit, ref missing);
			
		} 
		public void GoToTheBeginning( )
		{
			// VB : Selection.HomeKey Unit:=wdStory
			object unit ;
			unit = WdUnits.wdStory ;
			oWordApplic.Selection.HomeKey ( ref unit, ref missing);
			
		} 

		public void GoToTheTable(int ntable )
		{
			//	Selection.GoTo What:=wdGoToTable, Which:=wdGoToFirst, Count:=1, Name:=""
			//    Selection.Find.ClearFormatting
			//    With Selection.Find
			//        .Text = ""
			//        .Replacement.Text = ""
			//        .Forward = True
			//        .Wrap = wdFindContinue
			//        .Format = False
			//        .MatchCase = False
			//        .MatchWholeWord = False
			//        .MatchWildcards = False
			//        .MatchSoundsLike = False
			//        .MatchAllWordForms = False
			//    End With

			object what;
			what = WdUnits.wdTable ;
			object which;
			which = WdGoToDirection.wdGoToFirst;
			object count;
			count = 1 ;
			oWordApplic.Selection.GoTo( ref what, ref which, ref count, ref missing);
			oWordApplic.Selection.Find.ClearFormatting();

			oWordApplic.Selection.Text = "";
			
			
		} 

		public void GoToRightCell( )
		{
			// Selection.MoveRight Unit:=wdCell
			
//			object missing = System.Reflection.Missing.Value;
			object direction;
			direction = WdUnits.wdCell;
			oWordApplic.Selection.MoveRight(ref direction,ref missing,ref missing);
		} 

		public void GoToLeftCell( )
		{
			// Selection.MoveRight Unit:=wdCell
			
			object direction;
			direction = WdUnits.wdCell;
			oWordApplic.Selection.MoveLeft(ref direction,ref missing,ref missing);
		} 

		public void GoToDownCell( )
		{
			// Selection.MoveRight Unit:=wdCell
			
			object direction;
			direction = WdUnits.wdLine;
			oWordApplic.Selection.MoveDown(ref direction,ref missing,ref missing);
		} 

		public void GoToUpCell( )
		{
			// Selection.MoveRight Unit:=wdCell
			
			object direction;
			direction = WdUnits.wdLine;
			oWordApplic.Selection.MoveUp(ref direction,ref missing,ref missing);
		} 

	
		// this function doesn't work
		public void InsertPageNumber( string strType, bool bHeader )
		{
			object alignment ;
			object bFirstPage = false;
			object bF = true;
			//if (bHeader == true)
			//WordApplic.Selection.HeaderFooter.PageNumbers.ShowFirstPageNumber = bF;
			switch (strType)
			{
				case "Center":
					alignment = WdPageNumberAlignment.wdAlignPageNumberCenter;
					//WordApplic.Selection.HeaderFooter.PageNumbers.Add(ref alignment,ref bFirstPage);
					//Word.Selection objSelection = WordApplic.pSelection;
					
					oWordApplic.Selection.HeaderFooter.PageNumbers.Add(WdPageNumberAlignment.wdAlignPageNumberCenter);
					break;
				case "Right":
					alignment = WdPageNumberAlignment.wdAlignPageNumberRight;
					oWordApplic.Selection.HeaderFooter.PageNumbers.Add(WdPageNumberAlignment.wdAlignPageNumberRight);
					break;
				case "Left":
					alignment = WdPageNumberAlignment.wdAlignPageNumberLeft;
					oWordApplic.Selection.HeaderFooter.PageNumbers.Add(ref alignment,ref bFirstPage);
					break;
			}
            
		}

        public void CarregarDoc(string name)
        {
            //string name = @"C:\Documents\WordApplication3.doc";
            System.IO.FileStream fileStream = null;
            byte[] bytes = null;

            try
            {
                fileStream = new System.IO.FileStream(name, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                bytes = new byte[(int)fileStream.Length];
                fileStream.Read(bytes, 0, (int)fileStream.Length);

            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }

        }

	}
	//object units = WdUnits.wdCharacter;
	//object last=doc.Characters.Count;
	//doc.Range(ref first, ref last).Delete(ref units, ref last)
}
