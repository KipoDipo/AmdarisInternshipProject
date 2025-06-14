export type RegisterUserRequest = {
    username: string 
	displayName: string 
	email: string 
	password: string 

	firstName?: string
	lastName?: string
	bio?: string
    profilePicture?: File
	phoneNumber?: string
}