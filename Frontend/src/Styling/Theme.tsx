import { createTheme } from "@mui/material";

const colors = {
    yellow: '#FFFF69',
    yellowLight: '#FFFFAB',
    yellowDark: '#4f4f1f',

    white: '#FFFFFF',
    gray: '#3B3B3B',
    lightGray: '#6B6B6B',
    black: '#1B1B1B',
}

export const textWidth = 300;

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

    shape: {
        borderRadius: 7
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
        MuiFormLabel: {
            styleOverrides: {
                root: {
                    color: colors.white
                }
            }
        },
        MuiFormControl: {
            styleOverrides: {
                root: {
                    '& .MuiOutlinedInput-root': {
                        '& fieldset': {
                            borderColor: colors.lightGray,
                        },
                        '&:hover fieldset': {
                            borderColor: colors.yellow,
                        }
                    },
                }
            }
        },
        MuiSelect: {
            styleOverrides: {
                root: {
                    '& .MuiOutlinedInput-root': {
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
                    background: colors.black,
                },
            },
        },

        MuiAutocomplete: {
            styleOverrides: {
                root: {
                    '& .MuiOutlinedInput-root': {
                        '& fieldset': {
                            borderColor: colors.lightGray,
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
                        background: colors.black
                    },
                    '& .MuiFormLabel-root': {
                        color: colors.white,
                    },
                    '& .MuiSvgIcon-root': {
                        color: colors.white,
                    },
                },

                option: {
                    '&.Mui-focused': {
                        transition: 'background-color 0.1s ease',
                        backgroundColor: colors.lightGray,
                    },
                    '&[aria-selected="true"]': {
                        backgroundColor: colors.lightGray,
                    },
                }
            },
        },

        MuiTextField: {
            styleOverrides: {
                root: {
                    '& .MuiOutlinedInput-root': {
                        '& fieldset': {
                            borderColor: colors.lightGray,
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
                        '& textarea': {
                            color: colors.white
                        },
                        background: colors.black
                    },
                    '& .MuiFormLabel-root': {
                        color: colors.white,
                    },
                },
            },
        },

        MuiPaper: {
            styleOverrides: {
                root: {
                    backgroundColor: colors.black,
                    color: colors.white,
                }
            }
        },

        MuiDialog: {
            styleOverrides: {
                paper: {
                    backgroundColor: colors.gray,
                    color: colors.white,
                }
            }
        },

        MuiButton: {
            styleOverrides: {
                root: {
                    ":disabled": {
                        color: colors.lightGray,
                        backgroundColor: colors.black,
                        boxShadow: `0 0 10px ${colors.gray}`
                    }
                }
            }
        },

        MuiTableCell: {
            styleOverrides: {
                root: {
                    color: colors.white,
                    borderBottom: `1px solid ${colors.lightGray}`,
                },
                head: {
                    background: colors.lightGray
                },
            }
        },

        MuiTableRow: {
            styleOverrides: {
                root: {
                    background: colors.gray,
                    transition: 'background-color 0.3s ease',
                    "&:hover": {
                        background: colors.black
                    },
                },
            },
        },

        MuiSkeleton: {
            defaultProps: {
                animation: 'wave'
            },
            styleOverrides: {
                root: {
                    backgroundColor: colors.gray,
                },
            }
        }
    }
})
