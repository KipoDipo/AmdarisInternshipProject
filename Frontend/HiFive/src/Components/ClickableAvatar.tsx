import { Avatar, AvatarProps } from "@mui/material";
import ClickableBase from "./ClickableBase";

export default function ClickableAvatar(props : AvatarProps) {
    return (
        <ClickableBase>
            <Avatar {...props}/>
        </ClickableBase>
    )
}