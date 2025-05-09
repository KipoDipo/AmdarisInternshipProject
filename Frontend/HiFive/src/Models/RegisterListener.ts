export type RegisterListenerRequest = {
    username: string 
	displayname: string 
	email: string 
	password: string 

	firstName?: string
	lastName?: string
	bio?: string
    profilePicture?: File
	phoneNumber?: string
}