﻿using Grpc.Net.Client;
using myGrpc;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using T1Contracts.ServerCommunicationInterfaces;
using ItemLocation = Entities.Models.ItemLocation;

namespace GrpcClient.Clients {
	public class GrpcItemLocationClient : IItemLocationDataServerComm {
		
		private string _address;
		private GrpcChannel _channel;
		private ItemLocationService.ItemLocationServiceClient _client;

		public GrpcItemLocationClient(GRPCConnStr address) {
			_address = address.GrpcAddress;
		}

		// IEntityManager Override Methods
		public async Task<ItemLocation> RegisterAsync(ItemLocation entity) {
			// Convert Item to gRPC Item
			gItemLocation g = ConvertItemLocationToGItemLocation(entity);

			// Create Connection Point
			Connect( );

			// Send Call Request to Server and store reply
			var reply = await _client.RegisterItemLocationAsync(g);

			// Disconnect from Server
			await Disconnect( );

			// Convert returned gRPC Item to Item
			ItemLocation itemLocation = ConvertGItemLocationToItemLocation(reply);

			// Return Item to User
			return itemLocation;
		}

		public async Task<ItemLocation> RemoveAsync(int id) {
			// Convert Item to gRPC Item
			gItemLocationId g = new gItemLocationId {ItemLocationId = id};

			// Create Connection Point
			Connect( );

			// Send Call Request to Server and store reply
			var reply = await _client.RemoveItemLocationAsync(g);

			// Disconnect from Server
			await Disconnect( );

			// Convert returned gRPC Item to Item
			ItemLocation itemLocation = ConvertGItemLocationToItemLocation(reply);

			// Return Item to User
			return itemLocation;
		}

		public async Task<ItemLocation> UpdateAsync(ItemLocation entity) {
			// Convert Item to gRPC Item
			gItemLocation g = ConvertItemLocationToGItemLocation(entity);

			// Create Connection Point
			Connect( );

			// Send Call Request to Server and store reply
			var reply = await _client.UpdateItemLocationAsync(g);

			// Disconnect from Server
			await Disconnect( );

			// Convert returned gRPC Item to Item
			ItemLocation itemLocation = ConvertGItemLocationToItemLocation(reply);

			// Return Item to User
			return itemLocation;
		}

		public async Task<IList<ItemLocation>> GetAllAsync( ) {
			// Convert Item to gRPC Item | Here, it is specifically used as an Object Template for later
			gItemLocation template = new( ) { };

			// Create Connection Point
			Connect( );

			// Send Call Request to Server and store reply
			gItemLocationList reply = await _client.GetAllItemLocationsAsync(template);

			// Disconnect from Server
			await Disconnect( );

			// Generate Lists to read from, and fill in
			ICollection<gItemLocation> gItemLocations = reply.ItemLocations;
			List<ItemLocation> items = new( ) { };

			// Loop Through Collection of gItemLocations
			foreach (var g in gItemLocations) {
				// Convert each gItemLocation and add to list of Items
				items.Add(ConvertGItemLocationToItemLocation(g));
			}

			// Return Item to User
			return items;
		}

		public async Task<ItemLocation> GetAsync(int id) {
			// Convert Item to gRPC Item
			gItemLocationId g = new gItemLocationId {ItemLocationId = id};

			// Create Connection Point
			Connect( );

			// Send Call Request to Server and store reply
			var reply = await _client.GetItemLocationAsync(g);

			// Disconnect from Server
			await Disconnect( );

			// Convert returned gRPC Item to Item
			ItemLocation itemLocation = ConvertGItemLocationToItemLocation(reply);

			// Return Item to User
			return itemLocation;
		}

		public async Task<IList<ItemLocation>> GetAllByItemIdAsync(int itemId)
		{
			List<ItemLocation> itemLocations = new ();

			gItemId gItemId = new gItemId {ItemId = itemId};
			Connect();

			var reply = await _client.GetByItemIdAsync(gItemId);
			
			ICollection<gItemLocation> gItemLocations = reply.ItemLocations;
			// Loop Through Collection of gItemLocations
			foreach (var g in gItemLocations) {
				// Convert each gItemLocation and add to list of Items
				itemLocations.Add(ConvertGItemLocationToItemLocation(g));
			}
			
			await Disconnect();
			
			return itemLocations;
		}

		public async Task<IList<ItemLocation>> GetAllByLocationIdAsync(int locationId)
		{
			List<ItemLocation> itemLocations = new ();

			gLocationId gLocationId = new gLocationId {LocationId = locationId};
			
			Connect();

			var reply = await _client.GetByLocationIdAsync(gLocationId);
			
			ICollection<gItemLocation> gItemLocations = reply.ItemLocations;
			// Loop Through Collection of gItemLocations
			foreach (var g in gItemLocations) {
				// Convert each gItemLocation and add to list of Items
				itemLocations.Add(ConvertGItemLocationToItemLocation(g));
			}
			
			await Disconnect();
			
			return itemLocations;
		}


		private gItemLocation ConvertItemLocationToGItemLocation(ItemLocation from) {
			gItemLocation to = new( ) { Id = from.Id, Amount = from.Amount, Item = ConvertItemToGItem(from.Item), Location = ConvertLocationToGLocation(from.Location) };
			return to;
		}

		private ItemLocation ConvertGItemLocationToItemLocation(gItemLocation from) {
			ItemLocation to = new( ) { Id = from.Id, Amount = from.Amount, Item = ConvertGItemToItem(from.Item), Location = ConvertGLocationToLocation(from.Location) };
			return to;
		}
		
		private gLocation ConvertLocationToGLocation(Location from) {
			gLocation to = new( ) { Id = from.Id, Description = from.Description };
			return to;
		}

		private Location ConvertGLocationToLocation(gLocation from) {
			Location to = new( ) { Id = from.Id, Description = from.Description };
			return to;
		}
		private gItem ConvertItemToGItem(Item from) {
			gItem to = new( ) { Id = from.Id, ItemName = from.ItemName, Height = from.Height, Length = from.Length, Width = from.Width, Weight = from.Weight };
			return to;
		}

		private Item ConvertGItemToItem(gItem from) {
			Item to = new( ) { Id = from.Id, ItemName = from.ItemName, Height = from.Height, Length = from.Length, Width = from.Width, Weight = from.Weight };
			return to;
		}

		private void Connect( ) {
			_channel = GrpcChannel.ForAddress(_address);
			_client = new ItemLocationService.ItemLocationServiceClient(_channel);
		}

		private async Task Disconnect( ) {
			await _channel.ShutdownAsync( );
			_client = null;
		}

		
	}
}
