export type ListenerDetails = {
    id: string 

	displayName: string 

	bio?: string 
	firstName?: string 
	lastName?: string 
	profilePictureId?: string 

	createdPlaylistsIds: string[]

	equippedBadgeId?: string 
	badgeIds: string[]

	equippedTitleId?: string 
	titleIds: string[]

	followingArtists: string[]
	followingListeners: string[]
	followedByCount: number

	isSubscribed: boolean
}