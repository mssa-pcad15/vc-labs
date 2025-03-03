using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
;

namespace DemoAvalonia.ViewModels
{
    public class MembersViewModel : ReactiveObject
    {
        private readonly HttpClient _httpClient;
        private int _offset;
        private const int Limit = 10;

        public MembersViewModel()
        {
            _httpClient = new HttpClient();
            Members = new ObservableCollection<Member>();
            LoadMembersCommand = ReactiveCommand.CreateFromTask(LoadMembersAsync);
            LoadMembersCommand.Execute().Subscribe();
        }

        public ObservableCollection<Member> Members { get; }

        public ReactiveCommand<Unit, Unit> LoadMembersCommand { get; }

        private async Task LoadMembersAsync()
        {
            var response = await _httpClient.GetStringAsync($"https://aca-api-coreclaims-53eff.delightfulwave-7cf28c0a.eastus2.azurecontainerapps.io/api/members?offset={_offset}&limit={Limit}");
            var members = JsonSerializer.Deserialize<List<Member>>(response);
            Members.Clear();
            foreach (var member in members)
            {
                Members.Add(member);
            }
        }

        public void NextPage()
        {
            _offset += Limit;
            LoadMembersCommand.Execute().Subscribe();
        }

        public void PreviousPage()
        {
            if (_offset >= Limit)
            {
                _offset -= Limit;
                LoadMembersCommand.Execute().Subscribe();
            }
        }
    }

    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add other member properties as needed
    }
}