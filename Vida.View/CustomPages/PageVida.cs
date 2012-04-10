using System.IO;
using System;
using ViverMais.Model;
using System.Web.UI;
using System.Text.RegularExpressions;

public partial class PageViverMais : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    //protected override object LoadPageStateFromPersistenceMedium()
    //{
    //    try
    //    {
    //        if (Session.Count != 0)
    //        {
    //            string viewState = (string)Session[Session.SessionID];
    //            byte[] bytes = Convert.FromBase64String(viewState);
    //            bytes = Compressor.Decompress(bytes);

    //            LosFormatter formatter = new LosFormatter();

    //            return formatter.Deserialize(Convert.ToBase64String(bytes));
    //        }

    //        return base.LoadPageStateFromPersistenceMedium();

    //        //return new object();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //protected override void SavePageStateToPersistenceMedium(object state)
    //{
    //    try
    //    {
    //        if (Session.Count != 0)
    //        {
    //            LosFormatter formatter = new LosFormatter();
    //            StringWriter writer = new StringWriter();

    //            formatter.Serialize(writer, state);

    //            string viewStateString = writer.ToString();

    //            byte[] bytes = Convert.FromBase64String(viewStateString);
    //            bytes = Compressor.Compress(bytes);

    //            Session[Session.SessionID] = Convert.ToBase64String(bytes);
    //        }
    //        else
    //            base.SavePageStateToPersistenceMedium(state);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected override void Render(HtmlTextWriter writer)
    {
        MemoryStream memoryStream = new MemoryStream();
        try
        {
            using (StreamWriter streamWriter = new StreamWriter(memoryStream))
            {
                var textWriter = new HtmlTextWriter(streamWriter);
                base.Render(textWriter);
                textWriter.Flush();

                memoryStream.Position = 0;
                using (StreamReader reader = new StreamReader(memoryStream))
                {
                    var text = reader.ReadToEnd();
                    writer.Write(text);
                    reader.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            memoryStream.Dispose();
        }
    }
}