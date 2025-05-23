import { Avatar, Badge } from "@mui/material";
import { Badge as BadgeType } from "../Models/Badge"
import { ReactNode, useEffect, useState } from "react";
import { baseURL, fetcher } from "../Fetcher";

export default function AppBadge({ badgeId, children } : {badgeId: string | undefined, children: ReactNode}) {
  const [badge, setBadge] = useState<BadgeType>();

  useEffect(() => {
    fetcher.get(`Trophy/get-badge/${badgeId}`)
    .then(result => setBadge(result.data));
  }, [badgeId])

  return (
    <Badge
      overlap="circular"
      anchorOrigin={{ vertical: "top", horizontal: "right" }}
      badgeContent={
        badge &&
        <Avatar
          src={`${baseURL}Image/${badge.imageId}`}
          sx={{
            width: '45%',
            height: '45%',
          }}
        />
      }
      sx={{
        '& .MuiBadge-badge': {
          padding: 0,
          width: 'auto',
          height: 'auto',
        },
      }}
    >
      {children}
    </Badge>
  );
};