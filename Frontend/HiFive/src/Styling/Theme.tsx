import { createTheme } from "@mui/material";

const colors = {
  yellow: '#FFFF69',
  yellowLight: '#FFFFAB',
  yellowDark: '#FFFF34',

  white: '#FFFFFF',
  gray: '#3B3B3B',
  black: '#1B1B1B',
}

export const theme = createTheme({
  palette: {
    primary: {
      main: colors.yellow,
      light: colors.yellowLight,
      dark: colors.yellowDark,
    },
    secondary: {
      light: colors.white,
      main: colors.gray,
      dark: colors.black,
    }
  },

  components: {
    MuiTabs: {
      styleOverrides: {
        indicator: {
          display: 'none',
        }
      }
    },

    MuiTab: {
      styleOverrides: {
        root: {
          color: colors.white,
          '&.Mui-selected': {
            color: colors.black,
            backgroundColor: colors.yellow,
          },
        }
      }
    },

    MuiTypography: {
      styleOverrides: {
        root: {
          color: colors.white,
        }
      }
    },

    MuiAutocomplete: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            '& fieldset': {
              borderColor: colors.white,
            },
            '&:hover fieldset': {
              borderColor: colors.yellow,
            },
            '&.Mui-focused fieldset': {
              borderColor: colors.yellow,
            },
            '& input': {
              color: colors.white,
            },
          },
          '& .MuiFormLabel-root': {
              color: colors.white,
          },
          '& .MuiSvgIcon-root': {
            color: colors.white,
          },
        },
      },
    },

    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            '& fieldset': {
              borderColor: colors.white,
            },
            '&:hover fieldset': {
              borderColor: colors.yellow,
            },
            '&.Mui-focused fieldset': {
              borderColor: colors.yellow,
            },
            '& input': {
              color: colors.white,
            },
          },
          '& .MuiFormLabel-root': {
              color: colors.white,
          },
        },
      },
    }
  }
})
