import { Avatar, Badge } from "@mui/material";
import { Badge as BadgeType } from "../Models/Badge"
import { ReactNode, useEffect, useState } from "react";
import { fetcher } from "../Fetcher";
import FetchImage from "../Utils/FetchImage";

export default function AppBadge({ badgeId, children }: { badgeId: string | undefined | null, children: ReactNode }) {
  const [badge, setBadge] = useState<BadgeType>();

  useEffect(() => {
    if (badgeId === undefined || badgeId === null)
      setBadge(undefined);
    else
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
          src={FetchImage(badge.imageId)}
          sx={{
            width: '45%',
            height: '45%',
            boxShadow: 5,
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