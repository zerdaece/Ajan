using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Newtonsoft.Json;

public class LeaderboardManager : MonoBehaviour
{
    // Dashboard'da belirlediğin Leaderboard ID'sini buraya yaz
    private const string LeaderboardId = "Ajan_LeaderBoard"; 
    [SerializeField] private LeaderboardManager leaderboardManager;

    async void Start()
    {
       // 1. Servisleri başlat
    await UnityServices.InitializeAsync();

    // 2. Giriş yap ve işlemin bitmesini bekle
    if (!AuthenticationService.Instance.IsSignedIn)
    {
        try 
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Giriş Başarılı! Oyuncu ID: " + AuthenticationService.Instance.PlayerId);
            
            // Giriş başarılı olduktan SONRA tabloyu çekmeyi deneyebilirsin
            // GetLeaderboard(); 
        }
        catch (System.Exception e)
        {
            Debug.LogError("Giriş hatası: " + e.Message);
        }
    }
    }

    // --- SKOR GÖNDERME FONKSİYONU ---
    public async void SubmitScore(int score)
    {
        try
        {
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
            Debug.Log("Skor başarıyla gönderildi: " + score);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Skor gönderilirken hata oluştu: " + e.Message);
        }
    }

    // --- LİDERLİK TABLOSUNU ÇEKME FONKSİYONU ---
    public async void GetLeaderboard()
    {
        try
        {
            // İlk 10 kişiyi çekiyoruz
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Limit = 10 });
            
            Debug.Log("Liderlik Tablosu Getirildi:");
            
            foreach (var entry in scoresResponse.Results)
            {
                // entry.Rank: Sıralama (0'dan başlar, o yüzden +1 ekliyoruz)
                // entry.PlayerName: Oyuncu Adı
                // entry.Score: Yıldız Sayısı
                Debug.Log($"#{entry.Rank + 1} - Oyuncu: {entry.PlayerName} - Yıldız: {entry.Score}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Tablo çekilirken hata oluştu: " + e.Message);
        }
    }

    // --- OYUNCU ADINI GÜNCELLEME (İsteğe Bağlı) ---
    public async void UpdateDisplayName(string newName)
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(newName);
        Debug.Log("Oyuncu adı güncellendi: " + newName);
    }
}