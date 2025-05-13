type ArtistDetails = {
	id : string 

	displayName : string 

	profilePictureId? : string 
	bio? : string 
	firstName? : string 
	lastName? : string 

	albumIds : string[] 
	singleIds : string[] 
}

export type {ArtistDetails};