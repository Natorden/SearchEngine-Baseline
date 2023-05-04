namespace SearchHistoryApi.Interface;

public interface IHistory
{
    //This method call should retrieve a list of all searches
    //that have been done, as long as the application is running
    List<String> GetHistory();

    //This method call should save each search done, and could
    //optionally include more data, retrieved from each search
    //such as time taken, and results
    void SaveHistory(string s);
}