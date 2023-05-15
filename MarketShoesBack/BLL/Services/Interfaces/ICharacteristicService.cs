using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ICharacteristicService
    {
        Task<IEnumerable<Characteristic>> GetAllCharacteristicsAsync();

        Task<Characteristic?> GetCharacteristicAsync(int id);
        Task<Characteristic?> GetCharacteristicAsync(string characteristicName);

        Task<Characteristic?> CreateAsync(Characteristic characteristic);
        Task<Characteristic?> UpdateAsync(Characteristic characteristic, int id);


        //Sub
        Task<IEnumerable<SubCharacteristic>> GetSubCharacteristicsAsync(int characteristicId);

        Task<SubCharacteristic?> GetSubCharacteristicAsync(int id);


        Task<SubCharacteristic?> CreateAsync(SubCharacteristic characteristic);
        Task<SubCharacteristic?> UpdateAsync(SubCharacteristic characteristic, int id);



    }
}
